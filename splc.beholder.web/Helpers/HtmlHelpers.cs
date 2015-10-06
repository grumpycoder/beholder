using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Antlr.Runtime.Misc;

namespace splc.beholder.web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            var currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var builder = new TagBuilder("li")
                              {
                                  InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString()
                              };

            if (controllerName == currentController && actionName == currentAction)
                builder.AddCssClass("active");

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString Attr(this HtmlHelper helper, string name, string value, Func<bool> condition = null)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return MvcHtmlString.Empty;
            }

            var render = condition != null ? condition() : true;

            return render ?
                new MvcHtmlString(string.Format("{0}=\"{1}\"", name, HttpUtility.HtmlAttributeEncode(value))) :
                MvcHtmlString.Empty;
        }
    }
}
