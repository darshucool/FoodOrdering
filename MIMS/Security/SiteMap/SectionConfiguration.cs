using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Dinota.Core.Extensions;

namespace MIMS.Security.SiteMap
{
    public class SectionConfiguration
    {
        private readonly HashSet<ActionConfiguration> _nodes = new HashSet<ActionConfiguration>();

        private readonly Dictionary<Type, MethodInfo[]> _controllerCache;

        internal SectionConfiguration(Dictionary<Type, MethodInfo[]> controllerCache, string displayName)
        {
            _controllerCache = controllerCache;
            DisplayName = displayName;
        }

        internal string DisplayName { get; private set; }

        internal string Action { get; set; }

        internal Type ControllerType { get; set; }

        internal string IconName { get; set; }

        public SectionConfiguration Node<TController>(string displayName, string actionName)
            where TController : Controller
        {
            if (displayName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("displayName");

            if (actionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("actionName");

            var node = new ActionConfiguration(_controllerCache, displayName) { ControllerType = typeof(TController), Action = actionName };
            _nodes.Add(node);

            return this;

        }

        public SectionConfiguration Node<TController>(string displayName, string actionName, object routeValues)
           where TController : Controller
        {
            if (displayName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("displayName");

            if (actionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("actionName");

            var node = new ActionConfiguration(_controllerCache, displayName) { ControllerType = typeof(TController), Action = actionName, RouteValues = routeValues };
            _nodes.Add(node);

            return this;
        }

        public SectionConfiguration Node<TController>(string displayName, string actionName, string iconName, object routeValues)
            where TController : Controller
        {
            if (displayName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("displayName");

            if (actionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("actionName");

            if (iconName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("iconName");

            var node = new ActionConfiguration(_controllerCache, displayName) { ControllerType = typeof(TController), Action = actionName, IconName = iconName, RouteValues = routeValues };
            _nodes.Add(node);

            return this;
        }
         
        internal SectionNode BuildNode()
        {
            var section = new SectionNode(DisplayName) { Action = Action, IconName = IconName };

            section.Role = SiteMapUtil.GetRoleForAction(ControllerType, _controllerCache, Action);
            section.Controller = SiteMapUtil.GetControllerName(ControllerType);

            foreach (var actionConfiguration in _nodes)
            {
                section.Nodes.Add(actionConfiguration.BuildNode());
            }

            return section;
        }
    }
}