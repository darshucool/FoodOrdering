using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.Pagination;

namespace MIMS.Helpers
{
    public class AjaxPager<TModel> : IHtmlString
    {
        private readonly IPagination _pagination;
        private readonly AjaxHelper<TModel> _ajaxHelper;
        private readonly ViewContext _viewContext;
        private readonly bool _isLogGrid;
        private readonly string _divId = "popUp";
        private readonly string _onSuccess = "popUp(false);";
        private readonly string _actionName = string.Empty;
        private readonly Func<int, string, string> _urlBuilder;
        private string _paginationFormat = "Showing {0} - {1} of {2} ";
        private string _paginationSingleFormat = "Showing {0} of {1} ";
        private string _paginationFirst = "first";
        private string _paginationPrev = "prev";
        private string _paginationNext = "next";
        private string _paginationLast = "last";
        private string _pageQueryName = "page";
        private Func<int, string> _urlBuilderForPopUp;


        /// <summary>
        /// Creates a new instance of the AjaxPager class.
        /// </summary>
        /// <param name="pagination">The IPagination datasource</param>
        /// <param name="ajaxHelper">The AJAX Helper</param>
        /// <param name="divId">div tag id , ex:"#popUp"</param>
        /// <param name="onSuccess">This is for OnSuccess attribute of a link , ex:"popUp(true);"</param>
        public AjaxPager(IPagination pagination, AjaxHelper<TModel> ajaxHelper, string divId, string onSuccess)
        {
            _pagination = pagination;
            _ajaxHelper = ajaxHelper;
            _viewContext = ajaxHelper.ViewContext;

            _urlBuilderForPopUp = CreateDefaultUrlForPopUp;
            _divId = divId;
            _onSuccess = onSuccess;

            BuildModelRouteValues();
        }

        /// <summary>
        /// Creates a new instance of the AjaxPager class.
        /// </summary>
        /// <param name="pagination">The IPagination datasource</param>
        /// <param name="ajaxHelper">The AJAX Helper</param>
        /// <param name="divId">div tag id , ex:"#popUp"</param>
        /// <param name="onSuccess">This is for OnSuccess attribute of a link , ex:"popUp(true);"</param>
        /// <param name="actionName">actionName</param>
        public AjaxPager(IPagination pagination, AjaxHelper<TModel> ajaxHelper, string divId, string onSuccess, string actionName)
        {
            _pagination = pagination;
            _ajaxHelper = ajaxHelper;
            _viewContext = ajaxHelper.ViewContext;

            _urlBuilderForPopUp = CreateDefaultUrlForPopUp;
            _urlBuilder = CreateDefaultUrl;
            _divId = divId;
            _onSuccess = onSuccess;
            _actionName = actionName;

            BuildModelRouteValues();
        }

        /// <summary>
        /// Creates a new instance of the AjaxPager class.
        /// </summary>
        /// <param name="pagination">The IPagination datasource</param>
        /// <param name="ajaxHelper">The AJAX Helper</param>
        /// <param name="divId">div tag id , ex:"#popUp"</param>
        /// <param name="onSuccess">This is for OnSuccess attribute of a link , ex:"popUp(true);"</param>
        /// <param name="actionName">actionName</param>
        public AjaxPager(IPagination pagination, AjaxHelper<TModel> ajaxHelper, string divId, string onSuccess, string actionName, bool isLogGrid)
        {
            _pagination = pagination;
            _ajaxHelper = ajaxHelper;
            _viewContext = ajaxHelper.ViewContext;

            _urlBuilderForPopUp = CreateDefaultUrlForPopUp;
            _urlBuilder = CreateDefaultUrl;
            _divId = divId;
            _onSuccess = onSuccess;
            _actionName = actionName;

            _isLogGrid = isLogGrid;

            BuildModelRouteValues();
        }

        protected ViewContext ViewContext
        {
            get { return _viewContext; }
        }

        /// <summary>
        /// Specifies the query string parameter to use when generating AjaxPager links. The default is 'page'
        /// </summary>
        public AjaxPager<TModel> QueryParam(string queryStringParam)
        {
            _pageQueryName = queryStringParam;
            return this;
        }

        /// <summary>
        /// Specifies the format to use when rendering a pagination containing a single page. 
        /// The default is 'Showing {0} of {1}' (eg 'Showing 1 of 3')
        /// </summary>
        public AjaxPager<TModel> SingleFormat(string format)
        {
            _paginationSingleFormat = format;
            return this;
        }

        /// <summary>
        /// Specifies the format to use when rendering a pagination containing multiple pages. 
        /// The default is 'Showing {0} - {1} of {2}' (eg 'Showing 1 to 3 of 6')
        /// </summary>
        public AjaxPager<TModel> Format(string format)
        {
            _paginationFormat = format;
            return this;
        }

        /// <summary>
        /// Text for the 'first' link.
        /// </summary>
        public AjaxPager<TModel> First(string first)
        {
            _paginationFirst = first;
            return this;
        }

        /// <summary>
        /// Text for the 'prev' link
        /// </summary>
        public AjaxPager<TModel> Previous(string previous)
        {
            _paginationPrev = previous;
            return this;
        }

        /// <summary>
        /// Text for the 'next' link
        /// </summary>
        public AjaxPager<TModel> Next(string next)
        {
            _paginationNext = next;
            return this;
        }

        /// <summary>
        /// Text for the 'last' link
        /// </summary>
        public AjaxPager<TModel> Last(string last)
        {
            _paginationLast = last;
            return this;
        }

        /// <summary>
        /// Uses a lambda expression to generate the URL for the page links.
        /// </summary>
        /// <param name="urlBuilder">Lambda expression for generating the URL used in the page links</param>
        public AjaxPager<TModel> Link(Func<int, string> urlBuilder)
        {
            _urlBuilderForPopUp = urlBuilder;
            return this;
        }

        // For backwards compatibility with WebFormViewEngine
        public override string ToString()
        {
            return ToHtmlString();
        }

        public string ToHtmlString()
        {
            if (_pagination.TotalItems == 0)
            {
                return null;
            }

            var builder = new StringBuilder();

            builder.Append("<div class='pagination'>");
            if (!_isLogGrid || _pagination.TotalPages > 1)
            {
                RenderLeftSideOfPager(builder);
            }

            if (_pagination.TotalPages > 1)
            {
                RenderRightSideOfPager(builder);
            }

            builder.Append(@"</div>");

            return builder.ToString();
        }

        protected virtual void RenderLeftSideOfPager(StringBuilder builder)
        {
            builder.Append("<span class='paginationLeft'>");

            //Special case handling where the page only contains 1 item (ie it's a details-view rather than a grid)
            if (_pagination.PageSize == 1)
            {
                RenderNumberOfItemsWhenThereIsOnlyOneItemPerPage(builder);
            }
            else
            {
                RenderNumberOfItemsWhenThereAreMultipleItemsPerPage(builder);
            }
            builder.Append("</span>");
        }

        protected virtual void RenderRightSideOfPager(StringBuilder builder)
        {
            builder.Append("<span class='paginationRight'>");

            //If we're on page 1 then there's no need to render a link to the first page. 
            if (_pagination.PageNumber == 1)
            {
                builder.Append(_paginationFirst);
            }
            else
            {
                builder.Append(CreatePageLink(1, _paginationFirst));
            }

            builder.Append(" | ");

            //If we're on page 2 or later, then render a link to the previous page. 
            //If we're on the first page, then there is no need to render a link to the previous page. 
            if (_pagination.HasPreviousPage)
            {
                builder.Append(CreatePageLink(_pagination.PageNumber - 1, _paginationPrev));
            }
            else
            {
                builder.Append(_paginationPrev);
            }

            builder.Append(" | ");

            //Only render a link to the next page if there is another page after the current page.
            if (_pagination.HasNextPage)
            {
                builder.Append(CreatePageLink(_pagination.PageNumber + 1, _paginationNext));
            }
            else
            {
                builder.Append(_paginationNext);
            }

            builder.Append(" | ");

            int lastPage = _pagination.TotalPages;

            //Only render a link to the last page if we're not on the last page already.
            if (_pagination.PageNumber < lastPage)
            {
                builder.Append(CreatePageLink(lastPage, _paginationLast));
            }
            else
            {
                builder.Append(_paginationLast);
            }

            builder.Append("</span>");
        }

        protected virtual void RenderNumberOfItemsWhenThereIsOnlyOneItemPerPage(StringBuilder builder)
        {
            builder.AppendFormat(_paginationSingleFormat, _pagination.FirstItem, _pagination.TotalItems);
        }

        protected virtual void RenderNumberOfItemsWhenThereAreMultipleItemsPerPage(StringBuilder builder)
        {
            builder.AppendFormat(_paginationFormat, _pagination.FirstItem, _pagination.LastItem, _pagination.TotalItems);
        }

        private string CreatePageLink(int pageNumber, string text)
        {
            var builder = new TagBuilder("a");
            builder.SetInnerText(text);
            builder.MergeAttribute("data-ajax", "true");
            builder.MergeAttribute("data-ajax-mode", "replace");

            if (_onSuccess != "DivUpdate" && _actionName != "")
            {
                builder.MergeAttribute("href", _urlBuilder(pageNumber, _actionName));
                builder.MergeAttribute("data-ajax-failure", "serverError();");
                builder.MergeAttribute("data-ajax-success", _onSuccess);
            }
            else if (_onSuccess != "DivUpdate" && _actionName == "")
            {
                builder.MergeAttribute("href", _urlBuilderForPopUp(pageNumber));
                builder.MergeAttribute("data-ajax-failure", "serverError();");
                builder.MergeAttribute("data-ajax-success", _onSuccess);
            }
            else if (_onSuccess == "DivUpdate" && _actionName != "")
            {
                builder.MergeAttribute("href", _urlBuilder(pageNumber, _actionName));
                builder.MergeAttribute("data-ajax-failure", "serverError();");
                builder.MergeAttribute("data-ajax-loading", "#ajax-loading");
            }
            builder.MergeAttribute("data-ajax-update", "#" + _divId);

            return builder.ToString(TagRenderMode.Normal);
        }

        private RouteValueDictionary _modelRouteValues;

        private void BuildModelRouteValues()
        {
            var model = _ajaxHelper.ViewData.Model;

            if (model == null)
            {
                _modelRouteValues = new RouteValueDictionary();
                return;
            }

            _modelRouteValues = new RouteValueDictionary(model);

            var modelPIs = model.GetType().GetProperties();
            var dateProps = modelPIs.Where(pi => pi.PropertyType == typeof(DateTime)).ToList();
            var nullableDateProps = modelPIs.Where(pi => pi.PropertyType == typeof(DateTime?)).ToList();

            var culture = CultureInfo.CurrentCulture;
            var routeValues = new RouteValueDictionary();

            // if the model has datetime properties convert them to route value string using the culture used in server.

            foreach (var modelRouteValue in _modelRouteValues)
            {

                var dateProp = dateProps.Where(pi => pi.Name == modelRouteValue.Key).FirstOrDefault();
                if (dateProp != null)
                {
                    var date = (DateTime)dateProp.GetValue(model, null);
                    routeValues[modelRouteValue.Key] = date.ToString(culture.DateTimeFormat.ShortDatePattern);
                    continue;
                }
                else
                {
                    dateProp = nullableDateProps.Where(pi => pi.Name == modelRouteValue.Key).FirstOrDefault();

                    if (dateProp != null)
                    {
                        var date = dateProp.GetValue(model, null) as DateTime?;

                        if (date != null)
                        {
                            routeValues[modelRouteValue.Key] = date.Value.ToString(culture.DateTimeFormat.ShortDatePattern);
                            continue;
                        }
                    }
                }

                routeValues[modelRouteValue.Key] = modelRouteValue.Value;
            }

            _modelRouteValues = routeValues;
        }

        private string CreateDefaultUrlForPopUp(int pageNumber)
        {
            var routeValues = new RouteValueDictionary(_modelRouteValues);

            foreach (var key in _viewContext.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
            {
                if (routeValues[key] != null) routeValues[key] = _viewContext.RequestContext.HttpContext.Request.QueryString[key];
            }

            routeValues[_pageQueryName] = pageNumber;

            var url = UrlHelper.GenerateUrl(null, null, null, routeValues, RouteTable.Routes, _viewContext.RequestContext, true);
            return url;
        }

        private string CreateDefaultUrl(int pageNumber, string actionName)
        {
            var routeValues = new RouteValueDictionary(_modelRouteValues);

            foreach (var key in _viewContext.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
            {
                if (routeValues[key] != null) routeValues[key] = _viewContext.RequestContext.HttpContext.Request.QueryString[key];
            }

            routeValues[_pageQueryName] = pageNumber;

            var url = UrlHelper.GenerateUrl(null, actionName, null, routeValues, RouteTable.Routes, _viewContext.RequestContext, true);
            return url;
        }
    }
}