using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace MIMS.Security
{
    public abstract class SiteMapNode : IEquatable<SiteMapNode>
    {
        protected SiteMapNode(string displayName)
        {
            DisplayName = displayName;
            Nodes = new HashSet<SiteMapNode>();
        }

        public string DisplayName { get; private set; }

        public string Action { get; set; }

        internal object RouteValues { get; set; }

        public string Controller { get; set; }

        public string IconName { get; set; }

        public HashSet<SiteMapNode> Nodes { get; protected set; }

        public string Role { get; set; }

        public abstract void Render(StringCollection userRoles, ISiteMapRenderer siteMapRenderer, StringBuilder sb);

        public bool Equals(SiteMapNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.DisplayName, DisplayName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SiteMapNode)) return false;
            return Equals((SiteMapNode)obj);
        }

        public override int GetHashCode()
        {
            return DisplayName.GetHashCode();
        }
    }
}