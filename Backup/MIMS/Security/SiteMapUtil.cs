using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MIMS.Security
{
    public class SiteMapUtil
    {
        public static string GetRoleForAction(Type controllerType, Dictionary<Type, MethodInfo[]> controllerCache, string action)
        {
            if (controllerType == null)
                return string.Empty;

            if (!controllerCache.ContainsKey(controllerType))
            {
                controllerCache.Add(controllerType, controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public));
            }

            var methods = controllerCache[controllerType];

            if (methods.Length > 0)
            {
                var candidateMethods = methods.Where(mi => mi.Name == action &&
                    mi.GetCustomAttributes(typeof(HttpPostAttribute), false).Length == 0 &&
                    mi.GetCustomAttributes(typeof(DinotaAuthorizeAttribute), false).Length == 1);

                var methodCount = candidateMethods.Count();

                if (methodCount > 1)
                {
                    throw new AmbiguousMatchException(
                        string.Format("Multiple candidate methods found for action {0} on {1}", action, controllerType));
                }
                else if (methodCount == 1)
                {
                    var alfasiAuthorizeAttributes =
                        candidateMethods.First().GetCustomAttributes(typeof(DinotaAuthorizeAttribute), false);

                    var mdmAuthorizeAttribute = (DinotaAuthorizeAttribute)alfasiAuthorizeAttributes.First();

                    return string.Concat(mdmAuthorizeAttribute.FunctionalArea.ToString(), mdmAuthorizeAttribute.Writable ? "Write" : "Read");
                }
                else
                {
                    return string.Empty;
                }
            }

            throw new ArgumentException("Unable to find matching action method");
        }

        public static string GetControllerName(Type controllerType)
        {
            if (controllerType == null)
                return string.Empty;

            var index = controllerType.Name.IndexOf("Controller");

            return controllerType.Name.Substring(0, index);
        }
    }
}