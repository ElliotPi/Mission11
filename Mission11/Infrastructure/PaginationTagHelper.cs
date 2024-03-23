using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission11.Models.ViewModels;

namespace Mission11.Infrastructure
{
    // This attribute specifies that the PaginationTagHelper should be applied to <div> elements with the page-model attribute.
    // This means that the tag helper will only be triggered when it encounters a <div> tag with this attribute.
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;
        
        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            _urlHelperFactory = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; } // This gives us the info what view it's on when it is called
        public string? PageAction { get; set; } // This property holds the name of the action method to which the pagination links should point.
        public PaginationInfo PageModel { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; } = String.Empty;
        public string PageClassNormal {  get; set; } = String.Empty;
        public string PageClassSelected { get; set; } = String.Empty;

        // This method is called when the tag helper is processed. It generates the pagination links based on the PageModel and adds them to the output.
        public override void Process(TagHelperContext context, TagHelperOutput output) 
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i = 1; i <= PageModel.TotalNumPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });

                    if (PageClassesEnabled)
                    {
                        tag.AddCssClass(PageClass);
                        tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal); // if the page is sslected, use the selected; otherwise, use normal
                    }
                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);
                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
