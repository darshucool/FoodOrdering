using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using Dinota.Core.Extensions;

namespace MIMS.Security
{
    public class ActionNode : SiteMapNode
    {
        public ActionNode(string displayName)
            : base(displayName)
        {
        }

        public override void Render(StringCollection userRoles, ISiteMapRenderer siteMapRenderer, StringBuilder sb)
        {
            if (Role.IsNullOrEmpty() || userRoles.Contains(Role))
            {
                siteMapRenderer.RenderAction(this, sb);
            }
        }
    }
}