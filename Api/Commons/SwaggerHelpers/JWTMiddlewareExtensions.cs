using Asp.Versioning.ApiExplorer;

namespace Api.Commons.SwaggerHelpers
{
    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerUIApiVersioning(this IApplicationBuilder builder)
        {

            var provider = builder.ApplicationServices.GetService<IApiVersionDescriptionProvider>();

            builder.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return builder;

        }
    }
}
