using Microsoft.AspNetCore.Mvc;
using BookStore.WebUI.Areas.User.Models;
using App.Business.Abstract;

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

    public IActionResult Main()
    {
        IEnumerable<BookViewModel> booksList;
        booksList = (from b in _b?.GetList()
               join c in _c?.GetList()! on b.CategoryId equals c.Id
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

        return View(booksList);
    }
}