using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Dinota.Core.Extensions;

namespace MIMS.Security.SiteMap
{
    public class TopMenuRenderer : ISiteMapRenderer
    {
        private readonly HtmlHelper _htmlHelper;

        public TopMenuRenderer(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public void RenderMapStart(StringBuilder sb)
        {
            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("nav pull-right");
            sb.Append(ulTag.ToString(TagRenderMode.StartTag));
        }

        public void RenderMapEnd(StringBuilder sb)
        {
            var ulTag = new TagBuilder("ul");
            sb.Append(ulTag.ToString(TagRenderMode.EndTag));
        }

        public virtual void RenderSectionStart(SectionNode sectionNode, StringBuilder sb)
        {
            var liTag1 = new TagBuilder("li");
            liTag1.AddCssClass("divider-vertical");
            sb.Append(liTag1.ToString(TagRenderMode.StartTag));
            sb.Append(liTag1.ToString(TagRenderMode.EndTag));

            var liTag = new TagBuilder("li");


            liTag.AddCssClass("dropdown");
            liTag.GenerateId("me");
            

            sb.Append(liTag.ToString(TagRenderMode.StartTag));

            if (!sectionNode.Controller.IsNullOrWhiteSpace())
            {
               sb.Append(_htmlHelper.ActionLink(sectionNode.DisplayName, sectionNode.Action, sectionNode.Controller).ToHtmlString());
            }
            else
            {
                sb.Append(liTag.InnerHtml = string.Format("<a class='dropdown-toggle' data-toggle='dropdown' href=\"#\">"));

                var spanTag = new TagBuilder("span");
                spanTag.SetInnerText(sectionNode.Controller);
                spanTag.MergeAttribute("data-toggle", "tooltip");
                spanTag.MergeAttribute("title", sectionNode.Controller);
                sb.Append(spanTag.ToString(TagRenderMode.StartTag));

                sb.Append(sectionNode.DisplayName);

                var iconTag = new TagBuilder("b");
                iconTag.AddCssClass("caret");
                sb.Append(iconTag.ToString(TagRenderMode.StartTag));
                sb.Append(iconTag.ToString(TagRenderMode.EndTag));
                
                sb.Append(spanTag.ToString(TagRenderMode.EndTag));
                sb.Append("</a>");
            }
        }

        /// <summary>
        /// some sections may not have actions. this flag is used to avoid displaying empty menu
        /// </summary>
        private bool _sectionHasChildActions = false;

        public void RenderSectionEnd(SectionNode sectionNode, StringBuilder sb)
        {
            if (_sectionHasChildActions)
            {
                var ulTag = new TagBuilder("ul");
                sb.Append(ulTag.ToString(TagRenderMode.EndTag));
            }

            _sectionHasChildActions = false;
            var liTag = new TagBuilder("li");
            sb.Append(liTag.ToString(TagRenderMode.EndTag));
        }

        public void RenderAction(ActionNode actionNode, StringBuilder sb)
        {
            if (!_sectionHasChildActions)
            {
                var ulTag = new TagBuilder("ul");
                ulTag.AddCssClass("dropdown-menu pull-left");
                sb.Append(ulTag.ToString(TagRenderMode.StartTag));

                _sectionHasChildActions = true;
            }

            var liTag = new TagBuilder("li");

            liTag.InnerHtml = _htmlHelper.ActionLink(actionNode.DisplayName, actionNode.Action, actionNode.Controller, actionNode.RouteValues, null)
                    .ToHtmlString();

            sb.Append(liTag.ToString(TagRenderMode.Normal));
        }
    }
}