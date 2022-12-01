using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Dinota.Core.Extensions;

namespace MIMS.Security.SiteMap
{
    public class SiteMap
    {
        private HashSet<SectionConfiguration> _sectionConfigurations = new HashSet<SectionConfiguration>();

        private readonly HashSet<SectionNode> _sectionNodes = new HashSet<SectionNode>();

        private readonly Dictionary<Type, MethodInfo[]> _controllerCache;

        public SiteMap()
        {
            _controllerCache = new Dictionary<Type, MethodInfo[]>();
        }

        public SiteMap(Dictionary<Type, MethodInfo[]> controllerCache)
        {
            _controllerCache = controllerCache;
        }

        /// <summary>
        /// Creates a section with the given name
        /// </summary>
        /// <param name="displayName">Displayed as section heading</param>
        /// <returns></returns>
        public SectionConfiguration CreateSection(string displayName)
        {
            var config = new SectionConfiguration(_controllerCache, displayName);
            _sectionConfigurations.Add(config);

            return config;
        }

        /// <summary>
        /// Creates a section with the given name and icon
        /// </summary>
        /// <param name="displayName">Displayed as section heading</param>
        /// <param name="iconName">icon file name</param>
        /// <returns></returns>
        public SectionConfiguration CreateSection(string displayName, string iconName)
        {
            var config = new SectionConfiguration(_controllerCache, displayName) { IconName = iconName };
            _sectionConfigurations.Add(config);

            return config;
        }

        /// <summary>
        /// Creates a section as a link with the given name
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="displayName">Displayed as section heading</param>
        /// <param name="actionName">Controller action to link</param>
        /// <returns></returns>
        public SectionConfiguration CreateSection<TController>(string displayName, string actionName)
            where TController : Controller
        {
            if (displayName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("displayName");

            if (actionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("actionName");

            var config = new SectionConfiguration(_controllerCache, displayName) { ControllerType = typeof(TController), Action = actionName };
            _sectionConfigurations.Add(config);

            return config;
        }

        /// <summary>
        /// Creates a section as a link with the given name and icon
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="displayName">Displayed as section heading</param>
        /// <param name="actionName">Controller action to link</param>
        /// <param name="iconName">icon file name</param>
        /// <returns></returns>
        public SectionConfiguration CreateSection<TController>(string displayName, string actionName, string iconName)
            where TController : Controller
        {
            if (displayName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("displayName");

            if (actionName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("actionName");

            if (iconName.IsNullOrWhiteSpace())
                throw new ArgumentNullException("iconName");

            var config = new SectionConfiguration(_controllerCache, displayName) { ControllerType = typeof(TController), Action = actionName, IconName = iconName };
            _sectionConfigurations.Add(config);

            return config;
        }

        /// <summary>
        /// Builds up the map. One time operation
        /// </summary>
        public void Build()
        {
            _sectionNodes.Clear();

            foreach (var sectionConfiguration in _sectionConfigurations)
            {
                _sectionNodes.Add(sectionConfiguration.BuildNode());
            }

            _sectionConfigurations = null;
        }

        /// <summary>
        /// Renders the map with security trimming
        /// </summary>
        /// <param name="userRoles"></param>
        /// <param name="siteMapRenderer"></param>
        /// <returns></returns>
        public MvcHtmlString Render(StringCollection userRoles, ISiteMapRenderer siteMapRenderer)
        {
            var sb = new StringBuilder();

            siteMapRenderer.RenderMapStart(sb);
            RenderCore(userRoles, siteMapRenderer, sb);
            siteMapRenderer.RenderMapEnd(sb);

            return MvcHtmlString.Create(sb.ToString());
        }

        protected virtual void RenderCore(StringCollection userRoles, ISiteMapRenderer siteMapRenderer, StringBuilder sb)
        {
            foreach (var sectionNode in _sectionNodes)
            {
                if (!sectionNode.Role.IsNullOrEmpty() && !userRoles.Contains(sectionNode.Role))
                    continue;

                siteMapRenderer.RenderSectionStart(sectionNode, sb);
                sectionNode.Render(userRoles, siteMapRenderer, sb);
                siteMapRenderer.RenderSectionEnd(sectionNode, sb);
            }
        }
    }
}