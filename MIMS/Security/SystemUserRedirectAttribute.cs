using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MIMS.Util;
using Dinota.Core.Specification;
using Dinota.DataAccces.User;
using Dinota.Domain.User;

namespace AlfasiWeb.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SystemUserRedirectAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UserBase userBase;
            var userBaseService = new UserBaseService(new UserBaseRepo(DomainContextCreater.GetDomainContext));

            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                userBase = userBaseService.GetBy(new Specification<UserBase>(u => u.UserName == HttpContext.Current.User.Identity.Name));
            else
            {
                return;
            }

            if (!(userBase is ExternalUser))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home" }));
            }
        }

    }
}