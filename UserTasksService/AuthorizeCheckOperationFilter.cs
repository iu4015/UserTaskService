using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace UserTasksService
{
   	internal sealed class AuthorizeCheckOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			operation.Parameters.Add(new OpenApiParameter
			{
				Name = "Authorization",
				In = ParameterLocation.Header,
				Required = false
			});

			
		}
    }
}