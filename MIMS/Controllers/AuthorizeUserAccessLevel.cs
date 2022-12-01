using Dinota.Domain.PageObject;
using Dinota.Domain.User;
using Dinota.Domain.UserPermission;
using System.Web;
using System.Web.Mvc;
using MIMS.Helpers;

namespace MIMS.Controllers
{
    public class AuthorizeUserAccessLevel:AuthorizeAttribute
    {       
 
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            bool permission=false;
            //permission = new UserPermissionService().CheckPermission(DivisionId,PageName);
            string username = httpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                UserAccount user = new CustomDataBaseManger().SelectUser(username);
                PageObject oPageObject = new CustomDataBaseManger().SelectPageObject(currentAction, currentController);
                UserPermission oUserPermission = new CustomDataBaseManger().CheckUserPermission(user.UserTypeId, oPageObject.UId);
                if (oUserPermission.IsPermitted)
                    permission = true;
                else
                    permission = false;
            }
            else
            {
                permission = false;
            }
            return permission;
        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //you can change to any controller or html page.
            filterContext.Result = new RedirectResult("/Account/AccessDenied");
        }
    }
}