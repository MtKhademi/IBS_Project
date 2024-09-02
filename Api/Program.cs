//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Asp.Versioning;
using Hangfire;
using Microsoft.OpenApi.Models;
using Api.Commons.Attributes;
using Api.Commons.Extentions;
using Api.Commons.Middlewares;
using Api.Commons.SwaggerHelpers;
using Core.Modules.PatientModule;
using System.Text.Json.Serialization;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var allowEveryOne = "AllowEveryOne";
        var signalRPolicy = "SignalRPolicy";

        var builder = WebApplication.CreateBuilder(args);


        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Limits.MaxRequestBodySize = 1048576000;
            //52428800 
        });


        builder.WebHost.UseKestrel().UseIIS();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        builder.Services.AddCors(config =>
        {
            config.AddPolicy(allowEveryOne, policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
            config.AddPolicy(signalRPolicy,
                policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            .SetIsOriginAllowed(hotName => true);
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApplicationServices(builder.Configuration);

        builder.Services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));
        }).AddApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddSwaggerGen(config =>
        {
            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });
        builder.Services.ConfigureOptions<SwaggerApiVersioningConfig>();

        var app = builder.Build();

        app.UpdateMigration();

        // Configure the HTTP request pipeline.
        //if (!app.Environment.IsDevelopment())
        //{
        app.UseSwagger();
        //app.UseSwaggerUI();
        app.UseSwaggerUIApiVersioning();

        app.UseDeveloperExceptionPage();
        //}

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(signalRPolicy);
        app.UseJWTUserHandlerToken();
        app.UseGlobalExceptionHandler();
        app.UseJobRunnerHandler();


        app.UseHangfireDashboard("/hangfiredashboard", new DashboardOptions()
        {
            Authorization = new[] { new HangfierDashboardAuthorizationFilter() },
            IgnoreAntiforgeryToken = true
        });


        app.UseEndpoints(endpoints =>
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            //endpoints.MapHangfireDashboard("/");

        });

        app.MapFallbackToFile("index.html");

        app.Run();

    }
}
