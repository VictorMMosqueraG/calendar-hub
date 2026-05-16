namespace Api.Filters
{
    using System.Reflection;
    using Api.Attributes;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class RouteParameterDocumentationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var routeMappingAttributes = context.MethodInfo
                .GetCustomAttributes<RouteParameterMappingAttribute>()
                .ToList();

            if (routeMappingAttributes.Any() is false) return;

            foreach (var mapping in routeMappingAttributes)
            {
                var existingParameter = operation.Parameters?
                    .FirstOrDefault(p => p.Name == mapping.RouteParameterName && p.In == ParameterLocation.Path);

                if (existingParameter == null)
                {
                    operation.Parameters ??= [];
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = mapping.RouteParameterName,
                        In = ParameterLocation.Path,
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string" },
                        Description = $"ID del recurso padre"
                    });
                }
            }

            if (operation.RequestBody?.Content is not null)
            {
                foreach (var contentType in operation.RequestBody.Content.Keys)
                {
                    var mediaType = operation.RequestBody.Content[contentType];
                    RemovePropertiesFromSchema(mediaType.Schema, routeMappingAttributes, context);
                }

                if (HasNoRemainingProperties(operation.RequestBody, context))
                {
                    operation.RequestBody = null;
                }
            }
        }

        private static void RemovePropertiesFromSchema(OpenApiSchema schema, List<RouteParameterMappingAttribute> mappings, OperationFilterContext context)
        {
            if (schema == null) return;

            if (schema.Reference is not null)
            {
                var referencedSchemaId = schema.Reference.Id;
                if (context.SchemaRepository.Schemas.TryGetValue(referencedSchemaId, out var referencedSchema)) RemovePropertiesFromSchema(referencedSchema, mappings, context);
                return;
            }

            if (schema.Properties is not null)
            {
                foreach (var mapping in mappings)
                {
                    var snakeCaseProperty = ToSnakeCase(mapping.DtoPropertyName);

                    if (schema.Properties.ContainsKey(snakeCaseProperty))
                    {
                        schema.Properties.Remove(snakeCaseProperty);
                        schema.Required?.Remove(snakeCaseProperty);

                    }
                }
            }
        }

        private static bool HasNoRemainingProperties(OpenApiRequestBody requestBody, OperationFilterContext context)
        {
            return requestBody.Content.Values.All(content =>
            {
                var schema = ResolveSchema(content.Schema, context);
                return schema?.Properties is null || schema.Properties.Count == 0;
            });
        }

        private static OpenApiSchema? ResolveSchema(OpenApiSchema? schema, OperationFilterContext context)
        {
            if (schema?.Reference is null) return schema;

            return context.SchemaRepository.Schemas.TryGetValue(schema.Reference.Id, out var resolved)
                ? resolved
                : schema;
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLower(x) : char.ToLower(x).ToString()));
        }
    }
}