﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace BookStore.WebUI.TagHelpers
{
    [HtmlTargetElement("books-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("role")]
        public bool Role { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string role = string.Empty;
            if (Role)
                role = "admin";
            else
                role = "user";
            output.TagName = "section";
            var sb = new StringBuilder();
            sb.Append("<ul class='pagination'>");
            for (int i = 1; i <= PageCount; i++)
            {
                sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                sb.AppendFormat("<a class='page-link' href='/{0}?page={1}&category={2}'>{1}</a>", role, i, CurrentCategory);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

            output.Content.SetHtmlContent(sb.ToString());
        }
    }
}
