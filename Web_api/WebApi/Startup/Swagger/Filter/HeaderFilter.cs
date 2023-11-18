using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Startup.Swagger.Filter
{
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Currency",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
