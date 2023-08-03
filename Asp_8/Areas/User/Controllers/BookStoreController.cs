using Microsoft.AspNetCore.Mvc;
using Asp_8.Context;
using Asp_8.Areas.User.Models;

namespace Asp_8.Areas.User.Controllers;

[Area("User")]
public class BookStoreController : Controller
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    public BookStoreController(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;

        
    }

    public IActionResult Main()
    {
        IQueryable<BookViewModel> bvm;
        bvm = (from b in _bookStoreDbContext.Books
                               join c in _bookStoreDbContext.Categories! on b.CategoryId equals c.Id
                               join p in _bookStoreDbContext.Presses! on b.PressId equals p.Id
                               join t in _bookStoreDbContext.Themes! on b.ThemeId equals t.Id
                               join a in _bookStoreDbContext.Authors! on b.AuthorId equals a.Id
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

        return View("Main", bvm);
    }
}