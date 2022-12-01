using System.Web.Mvc;

namespace MIMS.Helpers
{
    public class FailMessage : Message
    {
        public FailMessage(string messageText)
            : base(messageText)
        {
        }

        public override MvcHtmlString Render(HtmlHelper htmlHelper)
        {
            var divTag = new TagBuilder("div");
            divTag.GenerateId("error_msg");
            divTag.AddCssClass("alert alert-error");
            divTag.SetInnerText(MessageText);

            var wrapperDivTag = new TagBuilder("div");
            wrapperDivTag.GenerateId("msg");
            wrapperDivTag.InnerHtml = divTag.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(wrapperDivTag.ToString(TagRenderMode.Normal));
        }
    }
}