//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Api.Commons.Middlewares;
using Api.Commons.Constants;

namespace Api.Commons.Middlewares
{
    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTUserHandlerToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTUserMiddleware>();
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }

        public static IApplicationBuilder UseSignalRCredentialHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SignalRMiddleware>();
        }

        public static IApplicationBuilder UseJobRunnerHandler(this IApplicationBuilder builder)
        {

            var hostingEnvironment = builder.ApplicationServices.GetService<IHostEnvironment>();
            if (hostingEnvironment.EnvironmentName == EnvironmentConstant.TestInMeMoryDb)
                return builder;

            return builder.UseMiddleware<JobRunnerMiddleware>();
        }
    }
}
