using System.Web.Mvc;

namespace MIMS.Helpers
{
    public abstract class Message
    {
        protected Message(string messageText)
        {
            MessageText = messageText;
        }

        public string MessageText { get; private set; }

        public abstract MvcHtmlString Render(HtmlHelper htmlHelper);
    }
}