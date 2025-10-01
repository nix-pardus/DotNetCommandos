using ServiceCenter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                var expression = BuildSingleFilterExpression(parameter, filter);

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
            var value = ConvertValue(filter.Value, property.Type);
            //TODO: привести в соответствие с перечислением
            return filter.Operator.ToUpper() switch
            {
                "EQUALS" => Expression.Equal(property, value),
                "NOTEQUALS" => Expression.NotEqual(property, value),
                "CONTAINS" => BuildContainsExpression(property, value),
                "GREATERTHAN" => Expression.GreaterThan(property, value),
                "LESSTHAN" => Expression.LessThan(property, value),
                "GREATERTHANOREQUAL" => Expression.GreaterThanOrEqual(property, value),
                "LESSTHANOREQUAL" => Expression.LessThanOrEqual(property, value),
                _ => throw new ArgumentException($"Unknown operator: {filter.Operator}")
            };
        }

        private ConstantExpression ConvertValue(string stringValue, Type targetType)
        {
            if (targetType == typeof(string))
                return Expression.Constant(stringValue);

            if (targetType == typeof(int) && int.TryParse(stringValue, out int intValue))
                return Expression.Constant(intValue);

            if (targetType == typeof(decimal) && decimal.TryParse(stringValue, out decimal decimalValue))
                return Expression.Constant(decimalValue);

            if (targetType == typeof(DateTime) && DateTime.TryParse(stringValue, out DateTime dateValue))
                return Expression.Constant(dateValue);

            if (targetType == typeof(bool) && bool.TryParse(stringValue, out bool boolValue))
                return Expression.Constant(boolValue);

            throw new ArgumentException($"Cannot convert value '{stringValue}' to type {targetType.Name}");
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
