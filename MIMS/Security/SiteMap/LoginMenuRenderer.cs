using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Dinota.Core.Extensions;

namespace MIMS.Security.SiteMap
{
    public class LoginMenuRenderer : TopMenuRenderer
    {
        private readonly HtmlHelper _htmlHelper;

        public LoginMenuRenderer(HtmlHelper htmlHelper) : base(htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }


        public override void RenderSectionStart(SectionNode sectionNode, StringBuilder sb)
        {
            var liTag1 = new TagBuilder("li");
            liTag1.AddCssClass("divider-vertical");
            sb.Append(liTag1.ToString(TagRenderMode.StartTag));
            sb.Append(liTag1.ToString(TagRenderMode.EndTag));

            var liTag = new TagBuilder("li");


            liTag.AddCssClass("dropdown");
            liTag.GenerateId("me");


            sb.Append(liTag.ToString(TagRenderMode.StartTag));

            if (!sectionNode.Controller.IsNullOrWhiteSpace() ) // i hve hard coded this. this is bad design
            {
                sb.Append(_htmlHelper.ActionLink(sectionNode.DisplayName, sectionNode.Action, sectionNode.Controller).ToHtmlString());
            }
            else
            {
                sb.Append(liTag.InnerHtml = string.Format("<a class='dropdown-toggle' data-toggle='dropdown' href=\"#\">"));

                var spanTag = new TagBuilder("span");
                spanTag.SetInnerText(sectionNode.Controller);
                if (sectionNode.DisplayName != "Dockets")
                {
                    spanTag.MergeAttribute("id", "user_name");
                    spanTag.AddCssClass("element");
                }
                spanTag.MergeAttribute("data-toggle", "tooltip");
                spanTag.MergeAttribute("data-placement", "top");
                spanTag.MergeAttribute("data-original-title", "log on user");
                spanTag.MergeAttribute("title", sectionNode.Controller);
                sb.Append(spanTag.ToString(TagRenderMode.StartTag));

                sb.Append(sectionNode.DisplayName);
                if(sectionNode.DisplayName!="Dockets")
                {
                    var iconTag = new TagBuilder("i");
                    iconTag.AddCssClass("icon-user");
                    sb.Append(iconTag.ToString(TagRenderMode.StartTag));
                    sb.Append(iconTag.ToString(TagRenderMode.EndTag));
                    
                }
                sb.Append(spanTag.ToString(TagRenderMode.EndTag));
                sb.Append("</a>");
            }
        }
       
    }
}