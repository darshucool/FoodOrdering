using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;

namespace MIMS.Helpers
{
    public class AjaxTableGridRenderer<T> : HtmlTableGridRenderer<T> where T : class
    {
        private readonly AjaxHelper _helper;
        private readonly string _divId = "popUp";
        private readonly string _actionName = string.Empty;

        private readonly List<PropertyInfo> _dateProps;
        private readonly List<PropertyInfo> _nullableDateProps;
        private readonly RouteValueDictionary _modelRouteValues;

        public AjaxTableGridRenderer(AjaxHelper helper, string divId, string actionName)
        {
            _helper = helper;
            _divId = divId;
            _actionName = actionName;

            _modelRouteValues = new RouteValueDictionary(_helper.ViewData.Model);

            var modelPIs = _helper.ViewData.Model.GetType().GetProperties();
            _dateProps = modelPIs.Where(pi => pi.PropertyType == typeof(DateTime)).ToList();
            _nullableDateProps = modelPIs.Where(pi => pi.PropertyType == typeof(DateTime?)).ToList();
        }

        public AjaxTableGridRenderer(AjaxHelper helper)
            : this(helper, "popUp", string.Empty)
        {
        }

        protected override void RenderHeaderText(GridColumn<T> column)
        {
            if (IsSortingEnabled && column.Sortable)
            {
                string sortColumnName = GenerateSortColumnName(column);

                bool isSortedByThisColumn = GridModel.SortOptions.Column == sortColumnName;

                var sortOptions = new GridSortOptions
                {
                    Column = sortColumnName
                };

                if (isSortedByThisColumn)
                {
                    sortOptions.Direction = (GridModel.SortOptions.Direction == SortDirection.Ascending)
                        ? SortDirection.Descending
                        : SortDirection.Ascending;
                }
                else //default sort order
                {
                    sortOptions.Direction = GridModel.SortOptions.Direction;
                }

                var routeValues = CreateRouteValuesForSortOptions(sortOptions, GridModel.SortPrefix);

                //Re-add existing querystring);
                foreach (var key in Context.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
                {
                    if (!routeValues.ContainsKey(key))
                    {
                        routeValues[key] = Context.RequestContext.HttpContext.Request.QueryString[key];
                    }
                }

                if (!string.IsNullOrWhiteSpace(_actionName))
                {
                    routeValues.Add("action", _actionName);
                }

                foreach (var routeData in Context.RouteData.Values)
                {
                    if (!routeValues.ContainsKey(routeData.Key))
                    {
                        routeValues[routeData.Key] = routeData.Value;
                    }
                }

                MergeModel(routeValues);

                if (column.DisplayName != null)
                {
                    string sortClass = GridModel.SortOptions.Direction == SortDirection.Ascending ? "sort_asc" : "sort_desc";

                    var link = _helper.RouteLink(column.DisplayName, routeValues, new AjaxOptions() { UpdateTargetId = _divId, InsertionMode = InsertionMode.Replace });

                    if (isSortedByThisColumn)
                    {
                        //bug fix: IE does not show sorted column indicator correctly
                        RenderText(string.Format("<div class=\"{0}\">", sortClass));
                        RenderText(link.ToString());
                        RenderText("</div>");
                    }
                    else
                    {
                        RenderText(link.ToString());
                    }
                }
            }
            else
            {
                base.RenderHeaderText(column);
            }
        }

        private static RouteValueDictionary CreateRouteValuesForSortOptions(GridSortOptions sortOptions, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return new RouteValueDictionary(sortOptions);
            }

            //There must be a nice way to do this...
            return new RouteValueDictionary(new Dictionary<string, object>()
            {
                { prefix + "." + "Column", sortOptions.Column },
                { prefix + "." + "Direction", sortOptions.Direction }
            });
        }

        private void MergeModel(RouteValueDictionary routeValues)
        {
            if (_helper.ViewData.Model == null) return;

            var culture = CultureInfo.CurrentCulture;

            foreach (var modelRouteValue in _modelRouteValues)
            {
                //Direction and Column route values are not insert in this method.. Because they are not common for all columns.
                if ((!routeValues.ContainsKey(modelRouteValue.Key) || routeValues[modelRouteValue.Key] != modelRouteValue.Value)
                    && modelRouteValue.Value != null && modelRouteValue.Key != "Direction" && modelRouteValue.Key != "Column")
                {
                    // if the model has datetime properties convert them to route value string using the culture used in server.
                    var dateProp = _dateProps.Where(pi => pi.Name == modelRouteValue.Key).FirstOrDefault();
                    if (dateProp != null)
                    {
                        var date = (DateTime)dateProp.GetValue(_helper.ViewData.Model, null);
                        routeValues[modelRouteValue.Key] = date.ToString(culture.DateTimeFormat.ShortDatePattern);
                        continue;
                    }
                    else
                    {
                        dateProp = _nullableDateProps.Where(pi => pi.Name == modelRouteValue.Key).FirstOrDefault();

                        if (dateProp != null)
                        {
                            var date = dateProp.GetValue(_helper.ViewData.Model, null) as DateTime?;

                            if (date != null)
                            {
                                routeValues[modelRouteValue.Key] = date.Value.ToString(culture.DateTimeFormat.ShortDatePattern);
                                continue;
                            }
                        }
                    }

                    routeValues[modelRouteValue.Key] = modelRouteValue.Value;
                }
            }
        }
    }
}