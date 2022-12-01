using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MIMS.Security.SiteMap
{
    internal class ActionConfiguration
    {
        private readonly Dictionary<Type, MethodInfo[]> _controllerCache;

        public ActionConfiguration(Dictionary<Type, MethodInfo[]> controllerCache, string displayName)
        {
            _controllerCache = controllerCache;
            DisplayName = displayName;
        }

        internal string DisplayName { get; private set; }

        internal string Action { get; set; }

        internal object RouteValues { get; set; }

        internal Type ControllerType { get; set; }

        internal string IconName { get; set; }

        internal ActionNode BuildNode()
        {
            var actionNode = new ActionNode(DisplayName) { RouteValues = RouteValues, Action = Action, IconName = IconName };
            actionNode.Role = SiteMapUtil.GetRoleForAction(ControllerType, _controllerCache, Action);
            actionNode.Controller = SiteMapUtil.GetControllerName(ControllerType);

            return actionNode;
        }
    }
}