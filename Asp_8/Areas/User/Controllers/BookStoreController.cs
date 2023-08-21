using Microsoft.AspNetCore.Mvc;
using BookStore.WebUI.Areas.User.Models;
using App.Business.Abstract;
using Asp_8.Entites;
using Microsoft.Extensions.Primitives;
using System.Web;

namespace BookStore.WebUI.Areas.User.Controllers;

[Area("User")]
public class BookStoreController : Controller
{
    private readonly ICategoryService? _c;
    private readonly IAuthorService? _a;
    private readonly IPressService? _p;
    private readonly IBooksService? _b;
    private readonly IThemeService? _t;

    public BookStoreController(ICategoryService? c, IAuthorService? a, IPressService? p, IBooksService? b, IThemeService? t)
    {
        _c = c;
        _a = a;
        _p = p;
        _b = b;
        _t = t;
    }

    public IActionResult Main(int page = 1, int category = 0)
    {
        int pageSize = 10, categoryCount = Convert.ToInt32(_c?.Context.Categories?.Count());

        IEnumerable<BookViewModel> booksList;

        BooksListViewModel model = new BooksListViewModel
        {
            Role = false
        };

        model.CurrentCategory = category > categoryCount ? categoryCount : category < 0 ? 1 : category;

        booksList = (from b in _b?.GetList()
                     join c in model.CurrentCategory == 0 ? _c?.GetList()! : _c?.GetList(c => c.Id == model.CurrentCategory)! on b.CategoryId equals c.Id
                     join p in _p?.GetList()! on b.PressId equals p.Id
                     join t in _t?.GetList()! on b.ThemeId equals t.Id
                     join a in _a?.GetList()! on b.AuthorId equals a.Id
                     select new BookViewModel
                     {
                         BookId = b.Id,
                         Name = b.Name,
                         Price = b.Price,
                         Count = b.Count,
                         Description = b.Description,
                         Category = c.Name,
                         AuthorName = a.Name,
                         AuthorSurname = a.Surname,
                         Theme = t.Name,
                         Press = p.Name
                     });

        model.PageCount = (int)Math.Ceiling(booksList.Count() / (double)pageSize);
        model.CurrentPage = model.PageCount < page ? model.PageCount : page <= 0 ? 1 : page; 
        model.Books = booksList.Skip((model.CurrentPage - 1) * pageSize).Take(pageSize);

        return View(model);
    }

    public string Buy()
    {
        return "Yes";
    }
}