using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dinota.Security;

namespace MIMS.Security
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DinotaAuthorizeAttribute : AuthorizeAttribute
    {
        private AuthorizationContext _authorizationContext;

        public DinotaAuthorizeAttribute(FunctionalAreas functionalArea)
        {
            FunctionalArea = functionalArea;
        }

        public FunctionalAreas FunctionalArea { get; private set; }

        public bool Writable { get; set; }

        public bool SetPermission { get; set; }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="httpContext"/> parameter is null.</exception>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Roles = string.Concat(FunctionalArea.ToString(), Writable ? "Write" : "Read");

            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            var userRoles = httpContext.Session[SessionKeys.Roles] as StringCollection;

            if (userRoles == null && !httpContext.User.IsInRole(Roles))
            {
                return false;
            }

            
           // if (userRoles != null && !userRoles.Contains(Roles) ) // commented by chamara 20140724 . this should be enable later
            if (userRoles != null && !userRoles.Contains(Roles) && Roles!="ProjectsHierarchyRead" )
            {
                return false;
            }

            if (SetPermission && _authorizationContext != null)
            {
                var writableRole = string.Concat(FunctionalArea.ToString(), "Write");

                var isWritable = userRoles != null ? userRoles.Contains(writableRole) : httpContext.User.IsInRole(writableRole);

                _authorizationContext.Controller.ViewData.Add(ViewDataKeys.Permission, new RolePermission(isWritable));
            }

            return true;
        }

        /// <summary>
        /// Called when a process requests authorization.
        /// </summary>
        /// <param name="filterContext">The filter context, which encapsulates information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>.</param><exception cref="T:System.ArgumentNullException">The <paramref name="filterContext"/> parameter is null.</exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            _authorizationContext = filterContext;

            base.OnAuthorization(filterContext);
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //http://aspnetwebstack.codeplex.com/SourceControl/changeset/view/1acb241299a8#src/System.Web.Mvc/AuthorizeAttribute.cs
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "AccessDenied" }));
        }
    }
}