//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using MDF.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationUserAttribute : Attribute, IAuthorizationFilter
    {
        public async void OnAuthorization(AuthorizationFilterContext context)
        {

            //-- check authenticaiton
            //-- then exist user in header
            var user = context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(ApiResultCreator.UnAuthorization());
                return;
            }



            //var path = context.HttpContext.Request.Path.Value;
            //path = path.Remove(0, path.IndexOf("/") + 1);
            //path = path.Remove(0, path.IndexOf("/") + 1);
            //path = path.Remove(0, path.IndexOf("/") + 1);

            //var existAnyPage = oUser?.Role?.RolePages?.Any(current => current.Page.Address.ToLower()
            //.Contains(path.ToLower())) ?? false;

            //if (!existAnyPage)
            //    context.Result = new JsonResult(ApiResultCreator.UnAuthorization());

        }
    }
}
