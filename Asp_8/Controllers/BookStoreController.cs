using Asp_8.Context;
using Asp_8.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Asp_8.Controllers;

public class BookStoreController : Controller
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    public BookStoreController(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;

        //_bookStoreDbContext.Add(new Category { Name = "SQL Language" });
        //_bookStoreDbContext.Add(new Category { Name = "C++ Builder" });
        //_bookStoreDbContext.Add(new Category { Name = "Delphi" });
        //_bookStoreDbContext.Add(new Category { Name = "Visual Basic" });
        //_bookStoreDbContext.Add(new Category { Name = "3D Studio Max" });
        //_bookStoreDbContext.Add(new Category { Name = "Mathcad" });
        //_bookStoreDbContext.Add(new Category { Name = "Novel" });
        //_bookStoreDbContext.Add(new Category { Name = "Mathematical Analysis" });

        //_bookStoreDbContext.Add(new Theme { Name = "Bases of data" });
        //_bookStoreDbContext.Add(new Theme { Name = "Graphic Packages" });
        //_bookStoreDbContext.Add(new Theme { Name = "High Mathematics" });
        //_bookStoreDbContext.Add(new Theme { Name = "Networks" });
        //_bookStoreDbContext.Add(new Theme { Name = "Web - design" });
        //_bookStoreDbContext.Add(new Theme { Name = "Windows 2000" });
        //_bookStoreDbContext.Add(new Theme { Name = "Operating systems" });

        //_bookStoreDbContext.Add(new Press { Name = "DiaSoft" });
        //_bookStoreDbContext.Add(new Press { Name = "BHV" });
        //_bookStoreDbContext.Add(new Press { Name = "Dialectics" });
        //_bookStoreDbContext.Add(new Press { Name = "Kudits - Image" });
        //_bookStoreDbContext.Add(new Press { Name = "Nauka" });
        //_bookStoreDbContext.Add(new Press { Name = "Binom" });

        //_bookStoreDbContext.Add(new Books { Name = "SQL", Count = 12, PressId = 1, ThemeId = 1, Description = "BestSeller", Price = 3.2M, CategoryId = 1 });
        //_bookStoreDbContext.Add(new Books { Name = "3D Studio Max 3", Count = 22, PressId = 2, ThemeId = 2, Description = "Sold", Price = 2.2M, CategoryId = 2 });
        //_bookStoreDbContext.Add(new Books { Name = "Visual Basic 6", Count = 15, PressId = 3, ThemeId = 1, Description = "BestSeller", Price = 1.8M, CategoryId = 5 });
        //_bookStoreDbContext.Add(new Books { Name = @"C++ Builder", Count = 14, PressId = 4, ThemeId = 4, Description = "BestSeller", Price = 3.3M, CategoryId = 4 });
        //_bookStoreDbContext.Add(new Books { Name = "Mathcad", Count = 21, PressId = 5, ThemeId = 3, Description = "BestSeller", Price = 4.2M, CategoryId = 3 });
        //_bookStoreDbContext.Add(new Books { Name = "Delphi helper", Count = 10, PressId = 3, ThemeId = 5, Description = "BestSeller", Price = 3.6M, CategoryId = 6 });

        //_bookStoreDbContext.Add(new Author { Name = "James", Surname = "Groff" });
        //_bookStoreDbContext.Add(new Author { Name = "Michael", Surname = "Marows" });
        //_bookStoreDbContext.Add(new Author { Name = "Boris", Surname = "Carpov" });
        //_bookStoreDbContext.Add(new Author { Name = "Vladimir", Surname = "Korol" });
        //_bookStoreDbContext.Add(new Author { Name = "Kevin", Surname = "Reichard" });
        //_bookStoreDbContext.Add(new Author { Name = "Olga", Surname = "Kokoreva" });

        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 1, AuthorId = 1 });
        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 2, AuthorId = 2 });
        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 3, AuthorId = 3 });
        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 4, AuthorId = 4 });
        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 5, AuthorId = 5 });
        //_bookStoreDbContext.Add(new AuthorBooks { BookId = 6, AuthorId = 6 });

        //_bookStoreDbContext.SaveChanges();
    }

    public IActionResult Main()
    {
        if(_bookStoreDbContext.Books != null) 
        {
            var anonymous = (from b in _bookStoreDbContext.Books
                             join c in _bookStoreDbContext.Categories! on b.CategoryId equals c.Id
                             join p in _bookStoreDbContext.Presses! on b.PressId equals p.Id
                             join t in _bookStoreDbContext.Themes! on b.ThemeId equals t.Id
                             join ab in _bookStoreDbContext.AuthorBooks! on b.Id equals ab.BookId
                             join a in _bookStoreDbContext.Authors! on ab.AuthorId equals a.Id
                select new
                {
                    Id = b.Id,
                    Name = b.Name,
                    Price = b.Price,
                    Count = b.Count,
                    Description = b.Description,
                    CategoryName = c.Name,
                    AuthorName = a.Name,
                    AuthorSurname = a.Surname,
                    ThemeName = t.Name,
                    PressName = p.Name
                });

            ViewBag.Dt = anonymous;
        }


        return View();
    }
}