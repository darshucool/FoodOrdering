using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Dinota.Security;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using MvcContrib.UI.Grid.Syntax;

namespace MIMS.Helpers
{
    public static class AjaxHelperExtensions
    {
        public static EnhancedAjaxHelper<TModel> Custom<TModel>(this AjaxHelper<TModel> helper)
        {
            return new EnhancedAjaxHelper<TModel>(helper);
        }

        public static IGrid<T> Grid<T>(this AjaxHelper helper, IEnumerable<T> dataSource, string divId, string actionName) where T : class
        {
            var grid = new Grid<T>(dataSource, helper.ViewContext);
            grid.RenderUsing(new AjaxTableGridRenderer<T>(helper, divId, actionName));

            return grid;
        }

        /// <summary>
        /// Creates a AjaxPager component using the specified IPagination as the datasource.
        /// </summary>
        /// <param name="helper">The Ajax Helper</param>
        /// <param name="pagination">The datasource</param>
        /// <returns>A Pager component</returns>
        /// <param name="divId">div tag id , ex:"#popUp"</param>
        /// <param name="onSuccess">This is for OnSuccess attribute of a link , ex:"popUp(true);"</param>
        public static AjaxPager<TModel> Pager<TModel>(this AjaxHelper<TModel> helper, IPagination pagination, string divId, string onSuccess)
        {
            return new AjaxPager<TModel>(pagination, helper, divId, onSuccess);
        }

        /// <summary>
        /// Use this method to popup a div.This will add the div element of the pop up
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="altText">alternative text to be used in image</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <param name="actionName">action to be executed</param>
        /// <param name="controllerName">name of the controller</param>
        /// <param name="routeValues">routes values to be passed to the controller method</param>
        /// <param name="ajaxOptions">ajax options collection</param>
        /// <param name="imageOrText">image or link of the text</param>
        /// <returns>html of the popup link + html of the popup div</returns>
        public static MvcHtmlString BoostrapedPopUpActionLink(this AjaxHelper helper, string altText, string updateDivId, string popupDivId, string title, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string imageOrText)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string link;

            if (!string.IsNullOrWhiteSpace(imageOrText.Split('.').Length > 1 ? imageOrText.Split('.')[1] : ""))
            {
                var builderImg = new TagBuilder("img");
                builderImg.MergeAttribute("src", urlHelper.Content("~/images/" + imageOrText));
                builderImg.MergeAttribute("alt", altText);
                builderImg.MergeAttribute("title", altText);

                link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, ajaxOptions).ToString();
                link = link.Replace("[replaceme]", builderImg.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                link = helper.ActionLink(imageOrText, actionName, controllerName, routeValues, ajaxOptions).ToString();
            }

            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("id", popupDivId);
            builderDiv.MergeAttribute("title", title);
            builderDiv.MergeAttribute("role", "dialog");
            builderDiv.MergeAttribute("tabindex", "true");
            builderDiv.MergeAttribute("aria-hidden", "-1");
            builderDiv.MergeAttribute("class", "modal hide fade");

            var headerDiv = new TagBuilder("div");
            headerDiv.MergeAttribute("class", "modal-header");

            var bodyDiv = new TagBuilder("div");
            bodyDiv.MergeAttribute("class", "modal-body");

            var updateDiv = new TagBuilder("div");
            updateDiv.MergeAttribute("id", updateDivId);

            var footerDiv = new TagBuilder("div");
            footerDiv.MergeAttribute("class", "modal-footer");

            var closeLink = new TagBuilder("a");
            closeLink.MergeAttribute("class", "close");
            closeLink.MergeAttribute("href", "#");
            closeLink.MergeAttribute("onclick", string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(link + builderDiv.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Use this method to popup a div.This will not add the div element of the pop up.use a seperate method to render popup div element
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="altText">alternative text to be used in image</param>
        /// <param name="actionName">action to be executed</param>
        /// <param name="controllerName">name of the controller</param>
        /// <param name="routeValues">routes values to be passed to the controller method</param>
        /// <param name="ajaxOptions">ajax options collection</param>
        /// <param name="imageOrText">image or link of the text</param>
        /// <returns>html of the popup link</returns>
        public static MvcHtmlString     BoostrapedPopUpActionLink(this AjaxHelper helper, string altText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string imageOrText)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string link;

            if (!string.IsNullOrWhiteSpace(imageOrText.Split('.').Length > 1 ? imageOrText.Split('.')[1] : ""))
            {
                var builderImg = new TagBuilder("img");
                builderImg.MergeAttribute("src", urlHelper.Content("~/images/" + imageOrText));
                builderImg.MergeAttribute("alt", altText);

                link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, ajaxOptions).ToString();
                link = link.Replace("[replaceme]", builderImg.ToString(TagRenderMode.SelfClosing));
            }
            else
            {
                link = helper.ActionLink(imageOrText, actionName, controllerName, routeValues, ajaxOptions).ToString();
            }

            return new MvcHtmlString(link);
        }

        public static MvcHtmlString BoostrapedDeletePopUpActionLink(this AjaxHelper helper,  object model)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string link;

            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                 ? (RolePermission)helper.ViewData[ViewDataKeys.Permission]
                                 : new RolePermission(false);

            if (rolePermission.CanEdit && rolePermission.CanDelete)
            {
                link =
                    helper.ActionLink("Delete", "Delete", model,
                                      new AjaxOptions()
                                          {
                                              UpdateTargetId = "DeleteDivContainer-Elements",
                                              OnSuccess = "javascript:BoostrapView('DeleteDivContainer');"
                                          }).ToString();
                return new MvcHtmlString(link);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString BoostrapedDeleteHierarchyPopUpActionLink(this AjaxHelper helper, object model)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string link;

            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                 ? (RolePermission)helper.ViewData[ViewDataKeys.Permission]
                                 : new RolePermission(false);

            if (rolePermission.CanEdit && rolePermission.CanDelete)
            {
                link =
                    helper.ActionLink("Delete", "DeleteHierarchy", model,
                                      new AjaxOptions()
                                      {
                                          UpdateTargetId = "DeleteDivContainer-Elements",
                                          OnSuccess = "javascript:BoostrapView('DeleteDivContainer');"
                                      }).ToString();

                return new MvcHtmlString(link);
            }
            return new MvcHtmlString(string.Empty);
        }

        //Delete Action Link With Icon
        public static MvcHtmlString IconDeletePopUpActionLink(this AjaxHelper helper, string actionName, string controllerName, object routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string link;

            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                 ? (RolePermission)helper.ViewData[ViewDataKeys.Permission]
                                 : new RolePermission(false);

            if (rolePermission.CanEdit && rolePermission.CanDelete)
            {
                link =
                    helper.ActionLink("[replaceme]", actionName, controllerName, routeValues,
                                      new AjaxOptions()
                                      {
                                          UpdateTargetId = "DeleteDivContainer-Elements",
                                          OnSuccess = "javascript:BoostrapView('DeleteDivContainer');"
                                      }).ToString();

                link = link.Replace("[replaceme]", "");

                return new MvcHtmlString(link);

            }
            return new MvcHtmlString(string.Empty);

        }

        public static MvcHtmlString TextBoxClear(this AjaxHelper helper, string controleId, string onclickText, string altText, string imageUrl)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            var builderimg = new TagBuilder("img");
            builderimg.MergeAttribute("src", urlHelper.Content("~/images/" + imageUrl));
            builderimg.MergeAttribute("alt", altText);
            builderimg.MergeAttribute("onclick", onclickText);

            var js = new StringBuilder("<script type=\"text/javascript\">");
            js.Append(Environment.NewLine);
            js.Append("function " + controleId + "Clear() {var items = " + controleId +
                      "Clear.arguments.length;  for (i = 0; i < items; i++) {if (i % 2 == 0) {ControleId = " +
                      controleId + "Clear.arguments[i];   }if (i % 2 == 1) {clearString = " +
                      controleId + "Clear.arguments[i];   document.getElementById(ControleId).value = clearString;}}}");
            js.Append(Environment.NewLine);
            js.Append("</script>");

            return new MvcHtmlString(builderimg.ToString(TagRenderMode.Normal).Replace("#", "'") + js);
        }
    }
}