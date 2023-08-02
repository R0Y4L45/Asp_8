using Asp_8.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Asp_8.TagHelpers;


[HtmlTargetElement("books")]
public class BooksTagHelper : TagHelper
{
    public Book_messageVM? Items { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tbody";
        var sb = new StringBuilder();

        foreach (BookViewModel item in Items?.BookViewModels!)
        {
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.BookId);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Name);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Category);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Theme);
            sb.AppendFormat("<td class=\"text-center\"> {0} {1} </td>", item.AuthorName, item.AuthorSurname);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Press);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Price);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Count);
            sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Description);
            sb.AppendFormat("<td class=\"text-center\"><ul class=\"list-inline mb-0\"><li class=\"list-inline-item dropdown\"><a class=\"text-muted dropdown-toggle font-size-18 px-2\" href=\"#\" role=\"button\" data-bs-toggle=\"dropdown\" aria-haspopup=\"true\"><i class=\"bx bx-dots-vertical-rounded\"></i></a><div class=\"dropdown-menu dropdown-menu-end\"><a class=\"dropdown-item link-danger\" href=\"/bookStore/delete/{0}\"> Delete </a><a class=\"dropdown-item link-info\" href = \"/bookStore/edit/{0}\"> Edit </a></div></li></ul></td>", item.BookId);
            sb.Append("</tr>");
        }

        output.PreContent.SetHtmlContent(sb.ToString());
    }
}
