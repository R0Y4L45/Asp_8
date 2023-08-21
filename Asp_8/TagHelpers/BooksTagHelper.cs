using BookStore.WebUI.Areas.User.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace BookStore.WebUI.TagHelpers;

[HtmlTargetElement("books")]
public class BooksTagHelper : TagHelper
{
    [HtmlAttributeName("Items")]
    public IEnumerable<BookViewModel>? Items { get; set; }

    [HtmlAttributeName("IdItem")]
    public int Id { get; set; } = default;

    [HtmlAttributeName("Area")]
    public bool? Area { get; set; } = default;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (Area != null)
        {
            output.TagName = "tbody";
            var sb = new StringBuilder();
            
            sb.Append("<thead class=\"thead-dark\">");
            sb.Append("<tr>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Id</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Name</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Category</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Theme</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Author</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Price</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Count</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Press</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\">Description</th>");
            sb.Append("<th class=\"text-center\" scope=\"col\" style=\"width: 200px;\">Action</th>\r\n");
            sb.Append("</tr>");
            sb.Append("</thead>");

            foreach (BookViewModel item in Items!)
            {
                if (Area == true)
                {
                    ScreeResultAdmin(sb, item);
                }
                else if (Area == false)
                {
                    ScreeResultUser(sb, item);
                }
            }

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    private StringBuilder ScreeResultUser(StringBuilder sb, BookViewModel item)
    {
        string? css;

        if (item.Count == 0)
            css = "disabled";
        else
            css = string.Empty;

        sb.Append("<tr>");
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.BookId);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Name);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Category);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Theme);
        sb.AppendFormat("<td class=\"text-center\"> {0} {1} </td>", item.AuthorName, item.AuthorSurname);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Price);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Count);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Press);
        sb.AppendFormat("<td class=\"text-center\"> {0} </td>", item.Description);
        sb.AppendFormat("<td class=\"text-center\"><ul class=\"list-inline mb-0\"><li class=\"list-inline-item dropdown\"><a class=\"text-muted dropdown-toggle font-size-18 px-2\" href=\"#\" role=\"button\" data-bs-toggle=\"dropdown\" aria-haspopup=\"true\"><i class=\"bx bx-dots-vertical-rounded\"></i></a><div class=\"dropdown-menu dropdown-menu-end\"><a class=\"dropdown-item link-info {1}\" href=\"/user/BookStore/Buy/{0}\"> Buy </a></div></li></ul></td>", item.BookId, css);
        sb.Append("</tr>");

        return sb;
    }

    private StringBuilder ScreeResultAdmin(StringBuilder sb, BookViewModel item)
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
        sb.AppendFormat("<td class=\"text-center\"><ul class=\"list-inline mb-0\"><li class=\"list-inline-item dropdown\"><a class=\"text-muted dropdown-toggle font-size-18 px-2\" href=\"#\" role=\"button\" data-bs-toggle=\"dropdown\" aria-haspopup=\"true\"><i class=\"bx bx-dots-vertical-rounded\"></i></a><div class=\"dropdown-menu dropdown-menu-end\"><a class=\"dropdown-item link-danger\" href=\"/admin/AdminBookStore/delete/{0}\"> Delete </a><a class=\"dropdown-item link-info\" href = \"/admin/AdminBookStore/edit/{0}\"> Edit </a></div></li></ul></td>", item.BookId);
        sb.Append("</tr>");

        return sb;
    }
}