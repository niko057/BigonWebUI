using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BigonWebUI.Helpers
{

    [HtmlTargetElement(Attributes = "asp-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-active-route")]
        public string Controller { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Controller.ToLower() == ViewContext.RouteData.Values["controller"].ToString().ToLower())
            {
                var classAttr = output.Attributes["class"];
                var activeClass = "active";

                if (classAttr == null || classAttr.Value == null || classAttr.Value.ToString().Trim() == "")
                {
                    output.Attributes.SetAttribute("class", activeClass);
                }
                else
                {
                    output.Attributes.SetAttribute("class", classAttr.Value.ToString() + " " + activeClass);
                }
            }
        }
    }

}
