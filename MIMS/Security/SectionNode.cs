using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace MIMS.Security
{
    public class SectionNode : SiteMapNode
    {
        public SectionNode(string displayName)
            : base(displayName)
        {
        }

        public override void Render(StringCollection userRoles, ISiteMapRenderer siteMapRenderer, StringBuilder sb)
        {
            foreach (var node in Nodes)
            {
                node.Render(userRoles, siteMapRenderer, sb);
            }
        }
    }
}