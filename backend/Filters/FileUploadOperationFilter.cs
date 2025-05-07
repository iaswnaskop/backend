namespace backend.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasFileParam = context.MethodInfo
            .GetParameters()
            .Any(p => p.ParameterType == typeof(IFormFile) ||
                      p.ParameterType == typeof(List<IFormFile>)
                 ); // αν χρησιμοποιείς JSON string

        if (!hasFileParam) return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type       = "object",
                        Properties = context.MethodInfo
                            .GetParameters()
                            .ToDictionary(
                                p => p.Name!,
                                p =>
                                {
                                    if (p.ParameterType == typeof(IFormFile))
                                        return new OpenApiSchema { Type = "string", Format = "binary" };
                                    if (p.ParameterType == typeof(List<IFormFile>))
                                        return new OpenApiSchema
                                        {
                                            Type  = "array",
                                            Items = new OpenApiSchema { Type = "string", Format = "binary" }
                                        };
                                    // όλα τα άλλα γίνονται string
                                    return new OpenApiSchema { Type = "string" };
                                }
                            )
                    }
                }
            }
        };
    }
}

