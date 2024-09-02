//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Models;

namespace Api.Commons.Extentions
{
    public static class JWTExtentions
    {
        public static void AttachUserToContext(this HttpContext context, IJWTUserHandlerService jWTHandlerService, string token)
        {
            try
            {
                var account = jWTHandlerService.GetModelFromToken(token);
                // attach user to context on successful jwt validation
                if (account.Id == 0 || account.UserName == "")
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
        public static UserManagementModel GetUserFromContext(this HttpContext context)
        {
            try
            {
                return (UserManagementModel)context.Items["User"];
            }
            catch (Exception ex)
            {
                throw new Exception("Can not read User from Contenxt", ex);
            }
        }

    }
}
