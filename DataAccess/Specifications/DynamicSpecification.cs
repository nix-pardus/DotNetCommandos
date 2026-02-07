using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceCenter.Infrascructure.DataAccess.Specifications
{
    internal class DynamicSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }

        public DynamicSpecification(IEnumerable<(string Field, string Operator, string Value)> filters, string logicalOperator)
        {
            Criteria = BuildExpression(filters, logicalOperator);
        }

        private Expression<Func<T, bool>> BuildExpression(IEnumerable<(string Field, string Operator, string Value)> filters, string logicalOperator)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression finalExpression = null;

            foreach (var filter in filters)
            {
                Expression expression;

                // Поддержка синтаксиса Field1||Field2 => (Field1 op value) OR (Field2 op value)
                if (!string.IsNullOrWhiteSpace(filter.Field) && filter.Field.Contains("||"))
                {
                    var parts = filter.Field.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(p => p.Trim())
                                            .Where(p => !string.IsNullOrEmpty(p))
                                            .ToArray();

                    Expression groupExpr = null;
                    foreach (var part in parts)
                    {
                        var single = BuildSingleFilterExpression(parameter, (part, filter.Operator, filter.Value));
                        groupExpr = groupExpr == null ? single : Expression.OrElse(groupExpr, single);
                    }

                    expression = groupExpr ?? Expression.Constant(true);
                }
                else
                {
                    expression = BuildSingleFilterExpression(parameter, filter);
                }

                if (finalExpression == null)
                {
                    finalExpression = expression;
                }
                else
                {
                    finalExpression = logicalOperator?.ToUpper() == "OR"
                        ? Expression.OrElse(finalExpression, expression)
                        : Expression.AndAlso(finalExpression, expression);
                }
            }

            return finalExpression == null
                ? x => true
                : Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
        }

        private Expression BuildSingleFilterExpression(ParameterExpression parameter, (string Field, string Operator, string Value) filter)
        {
            var property = Expression.Property(parameter, filter.Field);
            Type propertyType = property.Type;

            bool isEnum = propertyType.IsEnum;
            bool isNullableEnum = false;
            Type underlyingEnumType = null;

            if (!isEnum)
            {
                Type underlyingType = Nullable.GetUnderlyingType(propertyType);
                if (underlyingType != null && underlyingType.IsEnum)
                {
                    isNullableEnum = true;
                    underlyingEnumType = underlyingType;
                }
            }
            else
            {
                underlyingEnumType = propertyType;
            }

            if(filter.Operator.ToUpper() == "IN")
            {
                return BuildInExpression(property, filter.Value, propertyType, isEnum, isNullableEnum, underlyingEnumType);
            }

            Expression propertyForComparison = property;
            ConstantExpression value;

            if (isEnum || isNullableEnum)
            {
                if (isNullableEnum)
                {
                    propertyForComparison = Expression.Convert(property, typeof(int?));
                    value = ConvertEnumValueToInt(filter.Value, underlyingEnumType, isNullable: true);
                }
                else
                {
                    propertyForComparison = Expression.Convert(property, typeof(int));
                    value = ConvertEnumValueToInt(filter.Value, underlyingEnumType, isNullable: false);
                }
            }
            else if (filter.Operator.ToLower() == "isnull" || filter.Operator.ToLower() == "isnotnull")
            {
                return filter.Operator.ToUpper() switch
                {
                    "ISNULL" => BuildIsNullExpression(property),
                    "ISNOTNULL" => BuildIsNotNullExpression(property),
                    _ => throw new ArgumentException($"Unknown operator: {filter.Operator}")
                };
            }
            else
            {
                value = ConvertValue(filter.Value, property.Type);
            }

            //TODO: привести в соответствие с перечислением
            return filter.Operator.ToUpper() switch
            {
                "EQUALS" => Expression.Equal(propertyForComparison, value),
                "NOTEQUALS" => Expression.NotEqual(propertyForComparison, value),
                "CONTAINS" => BuildContainsExpression(property, value),
                "GREATERTHAN" => Expression.GreaterThan(property, value),
                "LESSTHAN" => Expression.LessThan(property, value),
                "GREATERTHANOREQUAL" => Expression.GreaterThanOrEqual(property, value),
                "LESSTHANOREQUAL" => Expression.LessThanOrEqual(property, value),
                "IN" => BuildInExpression(property, filter.Value, propertyType, isEnum, isNullableEnum, underlyingEnumType),
                _ => throw new ArgumentException($"Unknown operator: {filter.Operator}")
            };
        }

        private ConstantExpression ConvertValue(string stringValue, Type targetType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            bool isNullable = underlyingType != null;
            Type conversionType = isNullable ? underlyingType : targetType;

            if (conversionType == typeof(string))
                return Expression.Constant(stringValue);

            if (conversionType == typeof(int))
            {
                if (int.TryParse(stringValue, out int intValue))
                    return isNullable
                        ? Expression.Constant((int?)intValue)
                        : Expression.Constant(intValue);
                else if (isNullable)
                    return Expression.Constant(null);
            }

            if (conversionType == typeof(decimal))
            {
                if (decimal.TryParse(stringValue, out decimal decimalValue))
                    return isNullable
                        ? Expression.Constant((decimal?)decimalValue)
                        : Expression.Constant(decimalValue);
                else if (isNullable)
                    return Expression.Constant(null);
            }

            if (conversionType == typeof(DateTime))
            {
                if (DateTime.TryParse(stringValue, out DateTime dateValue))
                {
                    if (dateValue.Kind == DateTimeKind.Local)
                        dateValue = dateValue.ToUniversalTime();
                    else if (dateValue.Kind == DateTimeKind.Unspecified)
                        dateValue = DateTime.SpecifyKind(dateValue, DateTimeKind.Utc);

                    return isNullable
                        ? Expression.Constant((DateTime?)dateValue, targetType)
                        : Expression.Constant(dateValue, targetType);
                }
                else if (isNullable)
                    return Expression.Constant(null);
            }

            if (conversionType == typeof(bool))
            {
                if (bool.TryParse(stringValue, out bool boolValue))
                    return isNullable
                        ? Expression.Constant((bool?)boolValue)
                        : Expression.Constant(boolValue);
                else if (isNullable)
                    return Expression.Constant(null);
            }

            if (conversionType == typeof(Guid))
            {
                if (Guid.TryParse(stringValue, out Guid guidValue))
                    return isNullable
                        ? Expression.Constant((Guid?)guidValue)
                        : Expression.Constant(guidValue);
                else if (isNullable)
                    return Expression.Constant(null);
            }

            throw new ArgumentException($"Cannot convert value '{stringValue}' to type {targetType.Name}");
        }

        private Expression BuildInExpression(MemberExpression property, string valueString, Type propertyType, bool isEnum, bool isNullableEnum, Type underlyingEnumType)
        {
            if (string.IsNullOrWhiteSpace(valueString))
                return Expression.Constant(false);

            var stringValues = valueString.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            if(!stringValues.Any())
                return Expression.Constant(false);

            Expression result = null;

            foreach(var stringVal  in stringValues)
            {
                Expression equalExpression;

                if(isEnum || isNullableEnum)
                {
                    var value = ConvertEnumValueToInt(stringVal, underlyingEnumType, isNullableEnum);
                    Expression propertyForComparsion = isNullableEnum
                        ? Expression.Convert(property, typeof(int?))
                        : Expression.Convert(property, typeof(int));
                    equalExpression = Expression.Equal(propertyForComparsion, value);
                }
                else
                {
                    var value = ConvertValue(stringVal, propertyType);
                    equalExpression = Expression.Equal(property, value);
                }

                result = result == null
                    ? equalExpression
                    : Expression.OrElse(result, equalExpression);
            }

            return result ?? Expression.Constant(false);
        }

        private ConstantExpression ConvertEnumValueToInt(string stringValue, Type enumType, bool isNullable)
        {
            // Если строка пуста и это Nullable Enum, возвращаем null
            if (isNullable && string.IsNullOrEmpty(stringValue))
            {
                return Expression.Constant(null, typeof(int?));
            }

            int intValue;
            // Пытаемся преобразовать строку в число (если передано числовое значение)
            if (int.TryParse(stringValue, out intValue))
            {
                // Проверяем, что число является допустимым значением Enum
                if (!Enum.IsDefined(enumType, intValue))
                {
                    throw new ArgumentException($"Значение '{stringValue}' не является допустимым значением для {enumType.Name}");
                }
            }
            else
            {
                // Если строка — это имя элемента Enum (например, "Active")
                try
                {
                    var enumValue = Enum.Parse(enumType, stringValue, ignoreCase: true);
                    intValue = Convert.ToInt32(enumValue);
                }
                catch
                {
                    throw new ArgumentException($"Не удалось преобразовать значение '{stringValue}' в тип {enumType.Name}");
                }
            }

            return isNullable
                ? Expression.Constant(intValue, typeof(int?))
                : Expression.Constant(intValue);
        }

        private Expression BuildIsNullExpression(MemberExpression property)
        {
            Type propertyType = property.Type;

            if (propertyType == typeof(string))
            {
                var method = typeof(string).GetMethod("IsNullOrEmpty", new[] { typeof(string) });
                return Expression.Call(method, property);
            }

            if (propertyType.IsValueType && Nullable.GetUnderlyingType(propertyType) == null)
            {
                return Expression.Constant(false);
            }

            return Expression.Equal(property, Expression.Constant(null, propertyType));
        }

        private Expression BuildIsNotNullExpression(MemberExpression property)
        {
            Type propertyType = property.Type;

            if (propertyType == typeof(string))
            {
                var method = typeof(string).GetMethod("IsNullOrEmpty", new[] { typeof(string) });
                var call = Expression.Call(method, property);
                return Expression.Not(call);
            }

            if (propertyType.IsValueType && Nullable.GetUnderlyingType(propertyType) == null)
            {
                return Expression.Constant(true);
            }

            return Expression.NotEqual(property, Expression.Constant(null, propertyType));
        }

        private Expression BuildContainsExpression(MemberExpression property, ConstantExpression value)
        {
            if (property.Type != typeof(string))
            {
                throw new ArgumentException("Используется только со строковым типом");
            }
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(property, containsMethod, value);
        }

        private Expression BuildStartWithExpression(MemberExpression property, ConstantExpression value)
        {
            if (property.Type != typeof(string))
            {
                throw new ArgumentException("Используется только со строковым типом");
            }
            var containsMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            return Expression.Call(property, containsMethod, value);
        }
    }
}
