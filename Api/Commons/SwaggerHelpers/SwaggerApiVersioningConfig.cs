using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Commons.SwaggerHelpers
{
    public class SwaggerApiVersioningConfig : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        public SwaggerApiVersioningConfig(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Test API " + description.GroupName,
                Version = description.ApiVersion.ToString()
            };
            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
            return info;
        }
    }
}