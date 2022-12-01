using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

using MIMS.Util;
using Dinota.Core.Specification;
using Dinota.DataAccces.User;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Security;
using System.Web.Security;
using MIMS.Security.SiteMap;
using System.Drawing;
using AlfasiWeb.Models;
using Dinota.Domain.PageObject;
using Dinota.Domain.UserPermission;

namespace MIMS.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static EnhancedHtmlHelper<TModel> Custom<TModel>(this HtmlHelper<TModel> helper)
        {
            return new EnhancedHtmlHelper<TModel>(helper);
        }

        public static MvcHtmlString Message(this HtmlHelper helper)
        {
            if (helper.ViewContext.TempData.ContainsKey(ViewDataKeys.Message))
            {

                var message = (Message) helper.ViewContext.TempData[ViewDataKeys.Message];
                if (message != null)
                {
                    return message.Render(helper);
                }
            }

            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString GridLinks(this HtmlHelper helper, object model)
        {
            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                     ? (RolePermission) helper.ViewData[ViewDataKeys.Permission]
                                     : new RolePermission(false);

            if (rolePermission.CanEdit && rolePermission.CanDelete)
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                var editLinkTag = new TagBuilder("a");
                editLinkTag.MergeAttribute("href", urlHelper.Action("Edit", model));
                editLinkTag.SetInnerText("Edit");



                return
                    new MvcHtmlString(string.Format("{0}&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp",
                                                    editLinkTag.ToString(TagRenderMode.Normal)));
            }

            return new MvcHtmlString(string.Empty);
        }
        public static List<PageObject> getPageObjectList(this HtmlHelper helper, int parentId)
        {
            List<PageObject> objectGroup = new CustomDataBaseManger().SelectPageObject(parentId);
            return objectGroup;
        }
        public static UserPermission GetPermissionInfo(this HtmlHelper helper, int UserTypeId, int objectId)
        {
            UserPermission permissionGroup = new CustomDataBaseManger().SelectUserPermission(UserTypeId, objectId);
            return permissionGroup;
        }
        /// <summary>
        /// Calls the partial view to create new entity. A boolean view data item named 'ViewDataKeys.IsNew' is
        /// passed with the value 'true'
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MvcHtmlString PartialCreate(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            htmlHelper.ViewData[ViewDataKeys.IsNew] = true;

            return htmlHelper.Partial(partialViewName, model);
        }

        /// <summary>
        /// Calls the partial view to edit entity. A boolean view data item named 'ViewDataKeys.IsNew' is
        /// passed with the value 'false'
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="partialViewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MvcHtmlString PartialEdit(this HtmlHelper htmlHelper, string partialViewName, object model)
        {
            htmlHelper.ViewData[ViewDataKeys.IsNew] = false;

            return htmlHelper.Partial(partialViewName, model);
        }

        /// <summary>
        /// use this method to populate the div element of the bootstrap popup
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <returns>html of the popup div</returns>
        public static MvcHtmlString GetBootsrapDiv(this HtmlHelper helper, string updateDivId, string popupDivId,
                                                   string title)
        {
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
            closeLink.MergeAttribute("onclick",
                                     string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);


            return new MvcHtmlString(builderDiv.ToString(TagRenderMode.Normal));
        }


        /// <summary>
        /// use this method to populate the div element of the bootstrap popup for dockets 
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <returns>html of the popup div</returns>
        public static MvcHtmlString GetBootsrapDivDockets(this HtmlHelper helper, string updateDivId, string popupDivId,
                                                   string title)
        {
            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("id", popupDivId);
            builderDiv.MergeAttribute("title", title);
            builderDiv.MergeAttribute("role", "dialog");
            builderDiv.MergeAttribute("tabindex", "true");
            builderDiv.MergeAttribute("aria-hidden", "-1");
            builderDiv.MergeAttribute("class", "modal hide fade");
            builderDiv.MergeAttribute("style", "height:650px;overflow:scroll;width:950px; margin-left:-550px;");

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
            closeLink.MergeAttribute("onclick",
                                     string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);


            return new MvcHtmlString(builderDiv.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// use this method to populate the div element of the bootstrap popup -- design by sudesh (only fabrication)
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <returns>html of the popup div</returns>
        public static MvcHtmlString GetBootsrapDivSpecial(this HtmlHelper helper, string updateDivId, string popupDivId,
                                                   string title)
        {
            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("id", popupDivId);
            builderDiv.MergeAttribute("title", title);
            builderDiv.MergeAttribute("role", "dialog");
            builderDiv.MergeAttribute("tabindex", "true");
            builderDiv.MergeAttribute("aria-hidden", "-1");
            builderDiv.MergeAttribute("class", "modal hide fade");
            builderDiv.MergeAttribute("style", "height:450px;width:850px;");

            var headerDiv = new TagBuilder("div");
            headerDiv.MergeAttribute("class", "modal-header");
           

            var bodyDiv = new TagBuilder("div");
            bodyDiv.MergeAttribute("class", "modal-body");
           

            var updateDiv = new TagBuilder("div");
            updateDiv.MergeAttribute("id", updateDivId);
           

            //var footerDiv = new TagBuilder("div");
           // footerDiv.MergeAttribute("class", "modal-footer");

            var closeLink = new TagBuilder("a");
            closeLink.MergeAttribute("class", "close");
            closeLink.MergeAttribute("href", "#");
            closeLink.MergeAttribute("onclick",
                                     string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
           // builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);


            return new MvcHtmlString(builderDiv.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// use this method to populate the div element of the bootstrap popup -- small popup box for delete mark
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <returns>html of the popup div</returns>
        public static MvcHtmlString GetBootsrapDivSpecialDelete(this HtmlHelper helper, string updateDivId, string popupDivId,
                                                   string title)
        {
            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("id", popupDivId);
            builderDiv.MergeAttribute("title", title);
            builderDiv.MergeAttribute("role", "dialog");
            builderDiv.MergeAttribute("tabindex", "true");
            builderDiv.MergeAttribute("aria-hidden", "-1");
            builderDiv.MergeAttribute("class", "modal hide fade");
            builderDiv.MergeAttribute("style", "height:300px;width:500px;overflow:scroll;");

            var headerDiv = new TagBuilder("div");
            headerDiv.MergeAttribute("class", "modal-header");


            var bodyDiv = new TagBuilder("div");
            bodyDiv.MergeAttribute("class", "modal-body");


            var updateDiv = new TagBuilder("div");
            updateDiv.MergeAttribute("id", updateDivId);


            //var footerDiv = new TagBuilder("div");
            // footerDiv.MergeAttribute("class", "modal-footer");

            var closeLink = new TagBuilder("a");
            closeLink.MergeAttribute("class", "close");
            closeLink.MergeAttribute("href", "#");
            closeLink.MergeAttribute("onclick",
                                     string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
            // builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);


            return new MvcHtmlString(builderDiv.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// use this method to populate the div element of the bootstrap popup -- design by sudesh (only transport)
        /// </summary>
        /// <param name="helper">html helper</param>
        /// <param name="updateDivId">Div element to be update on link click</param>
        /// <param name="popupDivId">Div element to be pop up</param>
        /// <param name="title">title of the popup</param>
        /// <returns>html of the popup div</returns>
        public static MvcHtmlString GetBootsrapDivTrans(this HtmlHelper helper, string updateDivId, string popupDivId,
                                                   string title)
        {
            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("id", popupDivId);
            builderDiv.MergeAttribute("title", title);
            builderDiv.MergeAttribute("role", "dialog");
            builderDiv.MergeAttribute("tabindex", "true");
            builderDiv.MergeAttribute("aria-hidden", "-1");
            builderDiv.MergeAttribute("class", "modal hide fade");
            builderDiv.MergeAttribute("style", "height:600px;overflow:scroll;");

            var headerDiv = new TagBuilder("div");
            headerDiv.MergeAttribute("class", "modal-header");


            var bodyDiv = new TagBuilder("div");
            bodyDiv.MergeAttribute("class", "modal-body");
           

            var updateDiv = new TagBuilder("div");
            updateDiv.MergeAttribute("id", updateDivId);
         

            //var footerDiv = new TagBuilder("div");
            // footerDiv.MergeAttribute("class", "modal-footer");

            var closeLink = new TagBuilder("a");
            closeLink.MergeAttribute("class", "close");
            closeLink.MergeAttribute("href", "#");
            closeLink.MergeAttribute("onclick",
                                     string.Format("javascript:CloseDialog('{0}','{1}')", popupDivId, updateDivId));
            closeLink.InnerHtml = "&times;";

            var headerText = new TagBuilder("h3");
            headerText.SetInnerText(title);

            headerDiv.InnerHtml += closeLink.ToString(TagRenderMode.Normal);
            headerDiv.InnerHtml += headerText.ToString(TagRenderMode.Normal);

            bodyDiv.InnerHtml += updateDiv.ToString(TagRenderMode.Normal);

            builderDiv.InnerHtml += headerDiv.ToString(TagRenderMode.Normal);
            builderDiv.InnerHtml += bodyDiv.ToString(TagRenderMode.Normal);
            // builderDiv.InnerHtml += footerDiv.ToString(TagRenderMode.Normal);


            return new MvcHtmlString(builderDiv.ToString(TagRenderMode.Normal));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString GetDeleteDiv(this HtmlHelper helper)
        {
            return helper.GetBootsrapDiv("DeleteDivContainer-Elements", "DeleteDivContainer", "Delete");
        }
        public static int GetForceTypeCount(this HtmlHelper helper,int id)
        {
            int count = 0;
            count = new CustomDataBaseManger().SelectStrengthCount(id);
            return count;
        }
        public static MvcHtmlString GetDeleteDiv(this HtmlHelper helper, string title)
        {
            return helper.GetBootsrapDiv("DeleteDivContainer-Elements", "DeleteDivContainer", title);
        }

        public static string GetBase64StringImage(this HtmlHelper helper, byte[] imageFile)
        {
            if (imageFile == null)
            {
                //string path = "";
                //System.Drawing.Image img = System.Drawing.Image.FromFile(); 
                 ImageConverter converter = new ImageConverter();
                 byte[] newBA = (byte[])converter.ConvertTo("", typeof(byte[]));
                 return Convert.ToBase64String(newBA);
            }
            else
            {
                return Convert.ToBase64String(imageFile);
            }
        }

     

        public static string GetImagePath(this HtmlHelper helper, byte[] imageFile, string imageType)
        {
            var src = string.Format("data:image/{0};base64,{1}", imageType, Convert.ToBase64String(imageFile));

            return src;
        }
        public static UserAccount GetCurrentUserInfo(this HtmlHelper helper)
        {
            UserProfileSummry summary = new UserProfileSummry();
            UserAccount userAccount = null;
            var userAccountService = new UserAccountService(new UserAccountRepo(DomainContextCreater.GetDomainContext));

            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                userAccount =
                    userAccountService.GetBy(
                        new Specification<UserAccount>(u => u.UserName == HttpContext.Current.User.Identity.Name));
            }
            //summary = new CustomDataBaseManger().SelectUserProfile(userAccount.Id);
            return userAccount;
        }     
        public static MvcHtmlString FuzzyDate(this HtmlHelper helper, DateTime? dateTime)
        {
            if (dateTime != null)
            {
                var abbrTag = new TagBuilder("abbr");
                abbrTag.AddCssClass("timeago");
                abbrTag.Attributes.Add("title", dateTime.Value.ToString("o"));
                //abbrTag.InnerHtml += dateTime.Value.ToString(Properties.Resources.DateTimeFormat);

                return new MvcHtmlString(abbrTag.ToString());
            }

            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString TopMenu(this HtmlHelper helper)
        {
            return Configuration.Instance.TopMenu.Render(GetUserRoles(helper), new TopMenuRenderer(helper));
        }

        public static MvcHtmlString LoginMenu(this HtmlHelper helper)
        {
            return Configuration.Instance.LoginMenu.Render(GetUserRoles(helper), new LoginMenuRenderer(helper));
        }

        private static StringCollection GetUserRoles(HtmlHelper helper)
        {
            HttpContextBase httpContext = helper.ViewContext.HttpContext;

            if (!httpContext.Request.IsAuthenticated)
            {
                return new StringCollection();
            }

            var roles = httpContext.Session[SessionKeys.Roles] as StringCollection;

            if (roles == null)
            {
                string userName = httpContext.User.Identity.Name;
                roles = Roles.GetRolesForUser(userName).ToStringCollection();

                httpContext.Session[SessionKeys.Roles] = roles;
            }

            return roles;
        }

        /// <summary>
        /// check whether user is having the given user role
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public static bool IsUserAllowed(this HtmlHelper helper, string userRole)
        {
            HttpContextBase httpContext = helper.ViewContext.HttpContext;

            if (!httpContext.Request.IsAuthenticated)
            {
                return false;
            }

            var roles = httpContext.Session[SessionKeys.Roles] as StringCollection;

            if (roles == null)
            {
                string userName = httpContext.User.Identity.Name;
                roles = Roles.GetRolesForUser(userName).ToStringCollection();

                httpContext.Session[SessionKeys.Roles] = roles;
            }

            return roles.Contains(userRole);
        }

        private static StringCollection ToStringCollection(this IEnumerable<string> strings)
        {
            var collection = new StringCollection();

            foreach (string s in strings)
            {
                collection.Add(s);
            }

            return collection;
        }


        //Set Projects and Dynamic levels Home screen Images with styles
        public static MvcHtmlString ImageActionLink(this HtmlHelper helper, string tableName, byte[] image,
                                                    string imageType, string name, int uid, string actionName,
                                                    string controllerName, object routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var builderEm = new TagBuilder("em");
            builderEm.InnerHtml += name;
            builderEm.ToString(TagRenderMode.EndTag);

            var builderSpanFirst = new TagBuilder("span");
            builderSpanFirst.MergeAttribute("style", "z-index: 10; display: none;");
            builderSpanFirst.InnerHtml += builderEm;
            builderSpanFirst.ToString(TagRenderMode.EndTag);

            var builderSpanSecond = new TagBuilder("span");
            builderSpanSecond.MergeAttribute("class", "Big_title_bk_bottom");
            builderSpanSecond.MergeAttribute("id", "Big_title");
            builderSpanSecond.MergeAttribute("style", "z-index: 10; display: none;");
            builderSpanSecond.InnerHtml += builderEm;
            builderSpanSecond.ToString(TagRenderMode.EndTag);

            var builderImg = new TagBuilder("img");
            builderImg.MergeAttribute("style", "position: absolute; top: 0px; left: 0px;");
            builderImg.MergeAttribute("stop-opacity", "the opacity of this stop (0 to 1)");

            var browser = helper.ViewContext.HttpContext.Request.Browser;

            //this will be applied only for IE8 and lower version....
            if (browser.Browser.Trim().ToUpperInvariant().Equals("IE") && browser.MajorVersion <= 8)
            {
                builderImg.MergeAttribute("src",
                                          urlHelper.Content("~/ImageHandler.ashx?Uid=" + uid + "&TableName=" + tableName));
            }
            else
            {
                builderImg.MergeAttribute("src",
                                          string.Format("data:image/{0};base64,{1}", imageType,
                                                        helper.GetBase64StringImage(image)));
            }

            builderImg.MergeAttribute("height", "540");
            builderImg.MergeAttribute("width", "267");
            builderImg.ToString(TagRenderMode.SelfClosing);

            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("style", "position: absolute; top: 0px; left: 0px; width: 267px; height: 540px;");

            string link =
                helper.ActionLink("[replaceme]", actionName, controllerName, routeValues,
                                  new {style = "margin-left: 0px; margin-right: 0px;"}).ToString();
            link = link.Replace("[replaceme]", builderSpanFirst + builderSpanSecond.ToString() + builderImg + builderDiv);

            return new MvcHtmlString(link);
        }

        //Set Projects and Dynamic levels Home screen Images with styles
        public static MvcHtmlString ImageActionLinkForNew(this HtmlHelper helper, string tableName, byte[] image,
                                                    string imageType, string name, int uid, string actionName,
                                                    string controllerName, object routeValues)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var builderEm = new TagBuilder("em");
            builderEm.InnerHtml += name;
            builderEm.ToString(TagRenderMode.EndTag);

            var builderSpanFirst = new TagBuilder("span");
            builderSpanFirst.MergeAttribute("style", "z-index: 10; display: none;");
            builderSpanFirst.InnerHtml += builderEm;
            builderSpanFirst.ToString(TagRenderMode.EndTag);

            var builderSpanSecond = new TagBuilder("span");
            builderSpanSecond.MergeAttribute("class", "Big_title_bk_bottom");
            builderSpanSecond.MergeAttribute("id", "Big_title");
            builderSpanSecond.MergeAttribute("style", "z-index: 10; display: none;");
            builderSpanSecond.InnerHtml += builderEm;
            builderSpanSecond.ToString(TagRenderMode.EndTag);
            
            var builderImg = new TagBuilder("img");
            builderImg.MergeAttribute("style", "position: absolute; top: 0px; left: 0px;width:170px;height:170px;margin-top:7px;margin-left:7px");
            builderImg.MergeAttribute("stop-opacity", "the opacity of this stop (0 to 1)");

            var browser = helper.ViewContext.HttpContext.Request.Browser;

            //this will be applied only for IE8 and lower version....
            if (browser.Browser.Trim().ToUpperInvariant().Equals("IE") && browser.MajorVersion <= 8)
            {
                builderImg.MergeAttribute("src",
                                          urlHelper.Content("~/ImageHandler.ashx?Uid=" + uid + "&TableName=" + tableName));
            }
            else
            {
                builderImg.MergeAttribute("src",
                                          string.Format("data:image/{0};base64,{1}", imageType,
                                                        helper.GetBase64StringImage(image)));
            }

            builderImg.MergeAttribute("height", "170px");
            builderImg.MergeAttribute("width", "170px");
            builderImg.ToString(TagRenderMode.SelfClosing);
            var builderFigureFirst = new TagBuilder("div");
            builderFigureFirst.MergeAttribute("class", "fade");
            builderFigureFirst.InnerHtml += builderImg;
            builderFigureFirst.ToString(TagRenderMode.EndTag);
            var builderDiv = new TagBuilder("div");
            builderDiv.MergeAttribute("style", "position: absolute; top: 0px; left: 0px; width: 170px; height: 170px;");

            string link =
                helper.ActionLink("[replaceme]", actionName, controllerName, routeValues,
                                  new { style = "margin-left: 0px; margin-right: 0px;" }).ToString();
            link = link.Replace("[replaceme]", builderSpanFirst + builderSpanSecond.ToString() + builderFigureFirst);

            return new MvcHtmlString(link);
        }

        //Action Link With Icon
        public static MvcHtmlString IconEditActionLink(this HtmlHelper helper, string actionName, string controllerName,
                                                       object routeValues)
        {
            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                     ? (RolePermission) helper.ViewData[ViewDataKeys.Permission]
                                     : new RolePermission(false);
            if (rolePermission.CanEdit && rolePermission.CanEdit)
            {
                string link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, null).ToString();
                link = link.Replace("[replaceme]", "");

                return new MvcHtmlString(link);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString IconDetailActionLink(this HtmlHelper helper, string actionName,
                                                         string controllerName, object routeValues)
        {
            string link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, null).ToString();
            link = link.Replace("[replaceme]", "");

            return new MvcHtmlString(link);
        }

        public static MvcHtmlString EditBackActionLinks(this HtmlHelper helper, object model)
        {
            var rolePermission = helper.ViewData.ContainsKey(ViewDataKeys.Permission)
                                     ? (RolePermission) helper.ViewData[ViewDataKeys.Permission]
                                     : new RolePermission(false);

            if (rolePermission != null)
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

                var backLinkTag = new TagBuilder("a");
                backLinkTag.MergeAttribute("href", urlHelper.Action("Index", "Home"));
                backLinkTag.SetInnerText("Back to List");
                backLinkTag.Attributes.Add("color", "#005580");

                if (rolePermission.CanEdit)
                {
                    var editLinkTag = new TagBuilder("a");
                    editLinkTag.MergeAttribute("href", urlHelper.Action("Edit", model));
                    editLinkTag.SetInnerText("Edit");

                    return
                        new MvcHtmlString(string.Format("{0}&nbsp|&nbsp{1}", editLinkTag.ToString(TagRenderMode.Normal),
                                                        backLinkTag.ToString(TagRenderMode.Normal)));
                }

                return new MvcHtmlString(string.Format("{0}", backLinkTag.ToString(TagRenderMode.Normal)));
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString GetHtml(this HtmlHelper helper, string element)
        {
            return new MvcHtmlString(element);
        }


        public static MvcHtmlString GetDocketType(this HtmlHelper helper, int typeEnum)
        {
            string typeName = null;
            if (typeEnum == 1)
            {
                typeName = "Delivery docket";
            }
            if (typeEnum == 2)
            {
                typeName = "Call up list";
            }
            if (typeEnum == 3)
            {
                typeName = "Load list";
            }
            return new MvcHtmlString(typeName);
        }

        public static MvcHtmlString GetTrialAssy(this HtmlHelper helper, byte? val)
        {
            string typeName = null;
            if (val == 0)
            {
                typeName = "Not Started";
            }
            if (val == 50)
            {
                typeName = "Ongoing";
            }
            if (val == 100)
            {
                typeName = "Completed";
            }
            return new MvcHtmlString(typeName);
        }

        public static MvcHtmlString GetPackageStat(this HtmlHelper helper, int? val)
        {
            
            string typeName = null;
            if (val == 0)
            {
                typeName = "Not Started";
            }
            if (val == 50)
            {
                typeName = "Ongoing";
            }
            if (val == 100)
            {
                typeName = "Completed";
            }
            return new MvcHtmlString(typeName);
        }
        public static MvcHtmlString GetForceType(this HtmlHelper helper, int ForceTypeUId)
        {
            string ForceType = "";
            if (ForceTypeUId == 1)
                ForceType = "Army";
            else if (ForceTypeUId == 2)
                ForceType = "Navy";
            else if (ForceTypeUId == 3)
                ForceType = "Air Force";
            else if (ForceTypeUId == 4)
                ForceType = "Police";

            return new MvcHtmlString(ForceType);
        }

        public static bool IsSystemUser(this HtmlHelper helper)
        {
            UserBase userBase;
            var userBaseService = new UserBaseService(new UserBaseRepo(DomainContextCreater.GetDomainContext));

            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                userBase =
                    userBaseService.GetBy(
                        new Specification<UserBase>(u => u.UserName == HttpContext.Current.User.Identity.Name));
            }
            else
            {
                return false;
            }

            return !(userBase is ExternalUser)
                ;
        }
    }
}