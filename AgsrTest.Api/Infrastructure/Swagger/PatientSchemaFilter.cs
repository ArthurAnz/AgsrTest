using AgsrTest.Api.Models.Dtos;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AgsrTest.Api.Infrastructure.Swagger;

public class PatientSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(NameDto))
        {
            if (schema.Properties.TryGetValue("given", out var givenProp))
            {
                givenProp.MinItems = 0;
                givenProp.MaxItems = 2;

                givenProp.Example = new OpenApiArray
                {
                    new OpenApiString("Иван"),
                    new OpenApiString("Иванов"),
                };
            }
        }
    }
}
