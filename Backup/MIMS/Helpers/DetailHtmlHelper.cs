using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AlfasiWeb.Helpers
{
    public static class DetailHtmlHelper
    {
        private static readonly string EmptyValue = "&nbsp;";

        public static MvcHtmlString LabelFor(this HtmlHelper html, string expression)
        {
            var innerDiv = new TagBuilder("div");
            innerDiv.InnerHtml = "&nbsp;";
            return MvcHtmlString.Create(innerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DetailLabelFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                var innerDiv = new TagBuilder("div");
                innerDiv.InnerHtml = "&nbsp;";
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.InnerHtml = resolvedLabelText;
            tag.AddCssClass("text_label");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString GpScoordinates<TModel>(this HtmlHelper<TModel> helper, Func<TModel, string> expression)
        {
            var innerDiv = new TagBuilder("div");
            var value = expression(helper.ViewData.Model);
            if (value != null && !string.IsNullOrWhiteSpace(value))
            {
                innerDiv.Attributes.Add("title", "GPS Data is Captured");
                innerDiv.InnerHtml = "GPS Data is Captured";
            }
            else
            {
                innerDiv.Attributes.Add("title", "GPS Data isn't Captured");
                innerDiv.InnerHtml = "GPS Data is not Captured";
            }
            return MvcHtmlString.Create(innerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, string> expression, int length = 25)
        {
            var innerDiv = new TagBuilder("div");
            var value = expression(helper.ViewData.Model);
            if (value != null)
            {
                if (value.Length >= length)
                {
                    innerDiv.InnerHtml = value.Substring(0, length - 3) + "...";
                    innerDiv.Attributes.Add("title", value);
                }
                else
                {
                    innerDiv.InnerHtml = value;
                }
                if (value == "")
                    innerDiv.InnerHtml = "&nbsp;";
            }
            else
                innerDiv.InnerHtml = "&nbsp;";

            return MvcHtmlString.Create(innerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, int> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString(GetCurrentCulture().NumberFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, short> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString(GetCurrentCulture().NumberFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, byte> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString(GetCurrentCulture().NumberFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, float> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString(GetCurrentCulture().NumberFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, decimal> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString("C", GetCurrentCulture().NumberFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, DateTime> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value.ToString(GetCurrentCulture().DateTimeFormat));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, int?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString(GetCurrentCulture().NumberFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, short?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString(GetCurrentCulture().NumberFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, byte?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString(GetCurrentCulture().NumberFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, float?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString(GetCurrentCulture().NumberFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, decimal?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString("C", GetCurrentCulture().NumberFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, DateTime?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? nullable.Value.ToString(GetCurrentCulture().DateTimeFormat) : EmptyValue);
        }

        public static MvcHtmlString DetailForIf<TModel>(this HtmlHelper<TModel> helper, Func<TModel, object> nullableProperty, Func<TModel, string> expression, int length = 25)
        {
            var innerDiv = new TagBuilder("div");

            if (nullableProperty(helper.ViewData.Model) == null)
                //return MvcHtmlString.Create(EmptyValue);
                return MvcHtmlString.Create("&nbsp;");

            var value = expression(helper.ViewData.Model);
            if (value != null)
            {
                if (value.Length >= length)
                {
                    innerDiv.InnerHtml = value.Substring(0, length - 3) + "...";
                    innerDiv.Attributes.Add("title", value);
                }
                else
                {
                    innerDiv.InnerHtml = value;
                }
            }

            return MvcHtmlString.Create(innerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, bool> expression)
        {
            var value = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(value ? "Yes" : "No");
        }

        public static MvcHtmlString DetailFor<TModel>(this HtmlHelper<TModel> helper, Func<TModel, bool?> expression)
        {
            var nullable = expression(helper.ViewData.Model);

            return MvcHtmlString.Create(nullable.HasValue ? (nullable.Value ? "Yes" : "No") : EmptyValue);
        }

        private static CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentUICulture;
        }
    }
}