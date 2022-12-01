using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MvcContrib.UI.Grid.Syntax;

namespace MIMS.Helpers
{
    public class EnhancedHtmlHelper<TModel>
    {
        private const int TextFieldMaxLength = 25;
        private const int IntegerFieldLength = 25;
        private const int IntegerFieldMaxLength = 10;
        private const int ShortFieldMaxLength = 4; /* max allowed is 32767 so to be safe limit it to 4 */
        private const int ByteFieldMaxLength = 3;
        private const int DecimalFieldLength = 25;

        private readonly HtmlHelper<TModel> _helper;

        public EnhancedHtmlHelper(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return TextBoxFor(expression, new Dictionary<string, object>());
        }

        public IGrid<T> Grid<T>(IEnumerable<T> dataSource) where T : class
        {
            var grid = new MvcContrib.UI.Grid.Grid<T>(dataSource, _helper.ViewContext);
            grid.RenderUsing(new CustomGridRenderer<T>());

            return grid;
        }

        public IGrid<T> Grid<T>(IEnumerable<T> dataSource, string id, bool enableSearch) where T : class
        {
            var grid = new MvcContrib.UI.Grid.Grid<T>(dataSource, _helper.ViewContext);
            grid.RenderUsing(new CustomGridRenderer<T>(id, enableSearch));

            return grid;
        }

        

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes,int min,int max)
        {
            htmlAttributes.Add("data-val-range-min", min);
            htmlAttributes.Add("data-val-range-max", max);
            htmlAttributes.Add("data-val-range", string.Format("The field value must be between {0} and {1}.",min,max));

            return TextBoxFor(expression, htmlAttributes);
        }

        public MvcHtmlString TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            Type memberType = null;
          

            if (expression.NodeType == ExpressionType.Lambda && expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberType = expression.Body.Type;

                if (memberType.IsGenericType && memberType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    memberType = Nullable.GetUnderlyingType(memberType);
                }
            }
            else
            {
                return _helper.TextBoxFor(expression, htmlAttributes);
            }

            // to prevent unintended modifications to senders attribute collection
            htmlAttributes = new Dictionary<string, object>(htmlAttributes);

            const string rightAlign = "text_input_right";

            var typeCode = Type.GetTypeCode(memberType);
            switch (typeCode)
            {
                case TypeCode.Boolean:

                    break;

                case TypeCode.Byte:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.DateTime:
                    htmlAttributes.Add("readonly", "true");
                    htmlAttributes.Add("style", "width:50px;z-index:900;");

                    return _helper.EditorFor(expression, "DateTime", new { id = ExpressionHelper.GetExpressionText(expression) });

                case TypeCode.Decimal:
                    // special treatment to avoid 4 decimal points
                    return _helper.EditorFor(expression, "Money");

                case TypeCode.Double:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForDouble);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.Int16:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.Int32:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.Int64:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.SByte:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.Single:
                    htmlAttributes.Add("onkeypress", HtmlAttributesConstants.CheckForInt);
                    htmlAttributes.Add("onchange", HtmlAttributesConstants.CheckForNumber);
                    AddClass(htmlAttributes, rightAlign);
                    break;

                case TypeCode.String:

                    var memberAccessExpression = (MemberExpression)expression.Body;
                    var stringLengthAttribs = memberAccessExpression.Member.GetCustomAttributes(
                        typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute), true);

                    var length = ((System.ComponentModel.DataAnnotations.StringLengthAttribute)stringLengthAttribs[0]).MaximumLength;
                    MergeLength(htmlAttributes, TextFieldMaxLength, length);
                    break;

                // other cases are removed for clarity. add them as needed
            }

            return _helper.TextBoxFor(expression, htmlAttributes);
        }

        public MvcHtmlString TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            Type memberType = null;
            int? fieldMaxLength = 32;

            if (expression.NodeType == ExpressionType.Lambda && expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberType = expression.Body.Type;

                if (memberType.IsGenericType && memberType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    memberType = Nullable.GetUnderlyingType(memberType);
                }
            }
            else
            {
                return _helper.TextAreaFor(expression, htmlAttributes);
            }

            // to prevent unintended modifications to senders attribute collection            
            htmlAttributes = new Dictionary<string, object>(htmlAttributes);
            htmlAttributes.Add("onkeypress", HtmlAttributesConstants.PreventEnter);
            htmlAttributes.Add("onkeyup", HtmlAttributesConstants.PreventPaste);

            var typeCode = Type.GetTypeCode(memberType);
            switch (typeCode)
            {
                case TypeCode.String:

                    var memberAccessExpression = (MemberExpression)expression.Body;
                    var stringLengthAttribs = memberAccessExpression.Member.GetCustomAttributes(
                        typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute), true);

                    if (stringLengthAttribs.Length > 0)
                    {
                        var length = ((System.ComponentModel.DataAnnotations.StringLengthAttribute)stringLengthAttribs[0]).MaximumLength;

                        if (length > 0) fieldMaxLength = length;
                    }



                    MergeLength(htmlAttributes, TextFieldMaxLength, fieldMaxLength);
                    break;
            }

            return _helper.TextAreaFor(expression, htmlAttributes);
        }

        private static void AddClass(IDictionary<string, object> htmlAttributes, string @class)
        {
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] = string.Format("{0} {1}", htmlAttributes["class"], @class);
            }
            else
            {
                htmlAttributes["class"] = @class;
            }
        }

        public MvcHtmlString DescriptionTextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return TextBoxFor(expression, new Dictionary<string, object> { { "size", 60 } });
        }

        public MvcHtmlString LabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.IsRequired && typeof(TProperty) != typeof(bool))
            {
                var spanTag = new TagBuilder("span");
                spanTag.AddCssClass("required-star");
                spanTag.InnerHtml = "*";

                resolvedLabelText = string.Concat(resolvedLabelText, spanTag.ToString(TagRenderMode.Normal));
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(_helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.InnerHtml = resolvedLabelText;
            tag.AddCssClass("control-label");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public MvcHtmlString PasswordFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return _helper.PasswordFor(expression, new Dictionary<string, object>() { { "class", "input-xlarge" }, { "maxlength", "32" }, { "autocomplete", "off" } });
        }

        public MvcHtmlString FilterLabelFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(_helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.InnerHtml = resolvedLabelText;
            tag.AddCssClass("filter_label");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        private static void MergeLength(IDictionary<string, object> htmlAttributes, int size, int? maxLength)
        {
            if (!htmlAttributes.ContainsKey("maxlength") && maxLength.HasValue)
            {
                htmlAttributes["maxlength"] = maxLength.Value;
            }
        }

        internal static class HtmlAttributesConstants
        {
            public const string CheckForNumber = "return CheckForNumber(this)";
            public const string CheckForInt = "return CheckForInt(this,event)";
            public const string CheckForDouble = "return CheckForDouble(this,event)";
            public const string DatePicker = "datePicker hasDatepicker";
            public const string PreventEnter = "return PreventEnter(this,event)";
            public const string PreventPaste = "return PreventPaste(this)";
        }

        public MvcHtmlString LabelForLogin<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, _helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(resolvedLabelText))
            {
                return MvcHtmlString.Empty;
            }

            if (metadata.IsRequired && typeof(TProperty) != typeof(bool))
            {
                var spanTag = new TagBuilder("span");
                spanTag.AddCssClass("required-star");
                spanTag.InnerHtml = "*";

                resolvedLabelText = string.Concat(resolvedLabelText, spanTag.ToString(TagRenderMode.Normal));
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(_helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
            tag.InnerHtml = resolvedLabelText;
            tag.AddCssClass("input-block-level");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }


    }
}