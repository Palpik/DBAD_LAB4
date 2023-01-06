using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApp.ViewModels.PageViewModels;

namespace WebApp.Tag_Helpers;
public class PageLinkTagHelper : TagHelper
{
    private IUrlHelperFactory urlHelperFactory;
    public PageLinkTagHelper(IUrlHelperFactory helperFactory)
    {
        urlHelperFactory = helperFactory;
    }
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;
    public PageViewModel? PageModel { get; set; }
    public string PageAction { get; set; } = "";

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> PageUrlValues { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (PageModel == null) throw new Exception("PageModel is not set");
        IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        output.TagName = "div";

        TagBuilder tag = new TagBuilder("ul");
        tag.AddCssClass("pagination");

        TagBuilder currentItem = CreateTag(PageModel.PageNumber, urlHelper);

        if (PageModel.PageNumber > 3)
        {
            TagBuilder firstItem = CreateTag(1, urlHelper, "<");
            tag.InnerHtml.AppendHtml(firstItem);
        }

        if (PageModel.PageNumber > 2)
        {
            TagBuilder twoPrevItem = CreateTag(PageModel.PageNumber - 2, urlHelper);
            tag.InnerHtml.AppendHtml(twoPrevItem);
        }

        if (PageModel.PageNumber > 1)
        {
            TagBuilder prevItem = CreateTag(PageModel.PageNumber - 1, urlHelper);
            tag.InnerHtml.AppendHtml(prevItem);
        }

        tag.InnerHtml.AppendHtml(currentItem);

        if (PageModel.PageNumber < PageModel.TotalPages)
        {
            TagBuilder nextItem = CreateTag(PageModel.PageNumber + 1, urlHelper);
            tag.InnerHtml.AppendHtml(nextItem);
        }


        if (PageModel.PageNumber + 2 < PageModel.TotalPages)
        {
            TagBuilder twoNextItem = CreateTag(PageModel.PageNumber + 2, urlHelper);
            tag.InnerHtml.AppendHtml(twoNextItem);
        }

        if (PageModel.PageNumber + 3 < PageModel.TotalPages)
        {
            TagBuilder lastItem = CreateTag(PageModel.TotalPages, urlHelper, ">");
            tag.InnerHtml.AppendHtml(lastItem);
        }
        output.Content.AppendHtml(tag);
    }

    TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, string symbol = "")
    {
        if(symbol == "")
            symbol = pageNumber.ToString();
        TagBuilder item = new TagBuilder("li");
        TagBuilder link = new TagBuilder("a");
        if (pageNumber == PageModel?.PageNumber)
        {
            item.AddCssClass("active");
        }
        else
        {
            PageUrlValues["page"] = pageNumber;
            link.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
        }
        item.AddCssClass("page-item");
        link.AddCssClass("page-link");
        link.InnerHtml.Append(symbol);
        item.InnerHtml.AppendHtml(link);
        return item;
    }
}