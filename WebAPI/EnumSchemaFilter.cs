﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ServiceCenter.WebAPI;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if(context.Type.IsEnum)
        {
            schema.Enum.Clear();
            foreach(var enumName in Enum.GetNames(context.Type))
            {
                schema.Enum.Add(new OpenApiString(enumName));
            }
        }
    }
}
