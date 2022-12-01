using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MIMS.Security
{
    public interface ISiteMapRenderer
    {
        void RenderMapStart(StringBuilder sb);

        void RenderMapEnd(StringBuilder sb);

        void RenderSectionStart(SectionNode sectionNode, StringBuilder sb);

        void RenderSectionEnd(SectionNode sectionNode, StringBuilder sb);

        void RenderAction(ActionNode actionNode, StringBuilder sb);
    }
}