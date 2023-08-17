using BookStore.WebUI.Areas.User.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace BookStore.WebUI.TagHelpers;

[HtmlTargetElement("books")]
public class BooksTagHelper : TagHelper
{
    private const string itemsAttribute = "Items";
    [HtmlAttributeName(itemsAttribute)]
    public IEnumerable<BookViewModel>? Items { get; set; }

    private const string IdAttribute = "Id";
    [HtmlAttributeName(IdAttribute)]
    public int Id { get; set; } = default;

    private const string AreaAttribute = "Area";
    [HtmlAttributeName(AreaAttribute)]
    public bool? Area { get; set; } = default;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (Area != null)
        {
            output.TagName = "tbody";
            var sb = new StringBuilder();

            foreach (BookViewModel item in Items!)
            {
                if (Area == true)
                {
                    if (Id == 0)
                        ScreeResultAdmin(sb, item);
                    else if (Id != 0 && item.BookId == Id)
                        ScreeResultAdmin(sb, item);
                }
                else if (Area == false)
                {
                    if (Id == 0)
                        ScreeResultUser(sb, item);
                    else if (Id != 0 && item.BookId == Id)
                        ScreeResultUser(sb, item);
                }
            }

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

    private StringBuilder ScreeResultUser(StringBuilder sb, BookViewModel item)
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