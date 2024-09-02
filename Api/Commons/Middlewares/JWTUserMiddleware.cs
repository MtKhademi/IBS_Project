//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Models;

namespace Api.Commons.Middlewares
{
    public class JWTUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JWTUserMiddleware> _logger;

        public JWTUserMiddleware(RequestDelegate next, ILogger<JWTUserMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context, IJWTUserHandlerService jWTHandlerService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, jWTHandlerService, token);
            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IJWTUserHandlerService jWTHandlerService, string token)
        {
            try
            {
                var account = jWTHandlerService.GetModelFromToken(token);
                // attach user to context on successful jwt validation
                if (string.IsNullOrWhiteSpace(account?.UserName))
                    context.Items["User"] = null;
                else
                    context.Items["User"] = account;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

    }

}
