using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using MvcContrib.Pagination;

namespace MIMS.Helpers
{
    public class EnhancedAjaxHelper<TModel>
    {
        readonly AjaxHelper<TModel> _helper;

        public EnhancedAjaxHelper(AjaxHelper<TModel> helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// Creates a AjaxPager component using the specified IPagination as the datasource.
        /// </summary>
        /// <param name="pagination">The datasource</param>
        /// <param name="divId"></param>
        /// <param name="onSuccess"></param>
        /// <returns>A Pager component</returns>
        public AjaxPager<TModel> Pager(IPagination pagination, string divId, string onSuccess)
        {
            return _helper.Pager(pagination, divId, onSuccess);
        }

        /// <summary>
        /// Ajax BeginForm()
        /// </summary>
        /// <param name="actionName">action Name</param>
        /// <param name="controllerName">controller Name</param>
        /// <param name="routeValues"></param>
        /// <param name="popUpAjaxOptions"></param>
        /// <returns></returns>
        public MvcForm BeginForm(string actionName, string controllerName, object routeValues, AjaxOptions popUpAjaxOptions)
        {
            var routeValueDictionary = new RouteValueDictionary();

            foreach (var routeElement in (routeValues != null) ? new RouteValueDictionary(routeValues) : new RouteValueDictionary())
            {
                if (routeElement.Key != "Page")
                {
                    routeValueDictionary[routeElement.Key] = routeElement.Value;
                }
            }

            return _helper.BeginForm(actionName, controllerName, routeValueDictionary, popUpAjaxOptions);
        }
    }
}