using Asp_8.Context;
using Asp_8.Entites;
using Asp_8.Models;
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

    public IActionResult Main(string message)
    {
        Book_messageVM bmVm = new Book_messageVM();
        
        if (_bookStoreDbContext.Books != null)
        {
            bmVm.BookViewModels = (from b in _bookStoreDbContext.Books
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
            bmVm.Message = message;
        }

        return View("Main", bmVm);
    }

    public IActionResult Add() => View(new BookViewModel());

    [HttpPost]
    public IActionResult Add(BookViewModel bvm)
    {
        if (ModelState.IsValid)
        {
            bool flag = false;
            int aId = _bookStoreDbContext.Authors!.Where(aut => aut.Name == bvm.AuthorName && aut.Surname == bvm.AuthorSurname).Select(aut => aut.Id).FirstOrDefault();
            int cId = _bookStoreDbContext.Categories!.Where(c => c.Name == bvm.Category).Select(c => c.Id).FirstOrDefault();
            int tId = _bookStoreDbContext.Themes!.Where(t => t.Name == bvm.Theme).Select(t => t.Id).FirstOrDefault();
            int pId = _bookStoreDbContext.Presses!.Where(aut => aut.Name == bvm.Press).Select(p => p.Id).FirstOrDefault();

            if (aId == 0)
            {
                _bookStoreDbContext.Authors?.Add(new Author { Name = bvm.AuthorName, Surname = bvm.AuthorSurname });
                aId = _bookStoreDbContext.Authors!.Count() + 1;
            }
            if (cId == 0)
            {
                _bookStoreDbContext.Categories?.Add(new Category { Name = bvm.Category });
                cId = _bookStoreDbContext.Categories!.Count() + 1;
            }
            if (tId == 0)
            {
                _bookStoreDbContext.Themes?.Add(new Theme { Name = bvm.Theme });
                tId = _bookStoreDbContext.Themes!.Count() + 1;
            }
            if (pId == 0)
            {
                _bookStoreDbContext.Presses?.Add(new Press { Name = bvm.Press });
                pId = _bookStoreDbContext.Presses!.Count() + 1;
            }

            _bookStoreDbContext.SaveChanges();

            if (_bookStoreDbContext.Books!.Where(b => b.Name == bvm.Name && b.PressId == pId && b.CategoryId == cId && b.AuthorId == aId).FirstOrDefault() == null)
            {
                _bookStoreDbContext.Books?.Add(new Books
                {
                    Name = bvm.Name,
                    Price = bvm.Price,
                    Count = bvm.Count,
                    Description = bvm.Description ?? "",
                    PressId = pId,
                    CategoryId = cId,
                    ThemeId = tId,
                    AuthorId = aId
                });

                flag = true;
            }
            _bookStoreDbContext.SaveChanges();


            return RedirectToAction("Main", flag ? new { message = $"Book : {bvm.Name} added ;-)" } : new { message = $"Book : {bvm.Name} has been added by YOU ;-)" });
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        _bookStoreDbContext.Books!.Remove(_bookStoreDbContext.Books.Where(x => x.Id == id).First());
        _bookStoreDbContext.SaveChanges();
        return RedirectToAction("Main", new { message = "Book was deleted by YOU ;-(" });
    }

    public IActionResult Edit(int id = 0)
    {
        var bvm = (from b in _bookStoreDbContext.Books
                   join c in _bookStoreDbContext.Categories! on b.CategoryId equals c.Id
                   join p in _bookStoreDbContext.Presses! on b.PressId equals p.Id
                   join t in _bookStoreDbContext.Themes! on b.ThemeId equals t.Id
                   join a in _bookStoreDbContext.Authors! on b.AuthorId equals a.Id
                   where b.Id == id
                   select new BookViewModel
                   {
                       BookId = id,
                       Name = b.Name,
                       Price = b.Price,
                       Count = b.Count,
                       Description = b.Description,
                       Category = c.Name,
                       AuthorName = a.Name,
                       AuthorSurname = a.Surname,
                       Theme = t.Name,
                       Press = p.Name
                   }).FirstOrDefault();

        return View(bvm);
    }

    [HttpPost]
    public IActionResult Edit(BookViewModel bvm)
    {
        if (ModelState.IsValid)
        {
            Books? b = _bookStoreDbContext.Books?.First(b => b.Id == bvm.BookId);
            Category? c = _bookStoreDbContext.Categories?.First(c => c.Id == b!.CategoryId);
            Press? p = _bookStoreDbContext.Presses?.First(p => p.Id == b!.PressId);
            Theme? t = _bookStoreDbContext.Themes?.First(t => t.Id == b!.ThemeId);
            Author? a = _bookStoreDbContext.Authors?.First(a => a.Id == b!.AuthorId);
            b!.Name = bvm.Name;
            b.Price = bvm.Price;
            b.Count = bvm.Count;
            b.Description = bvm.Description;
            c!.Name = bvm.Category;
            p!.Name = bvm.Press;
            t!.Name = bvm.Theme;
            a!.Name = bvm.AuthorName;
            a.Surname = bvm.AuthorSurname;

            _bookStoreDbContext.SaveChanges();

            return RedirectToAction("Main", new { message = $"Book => {bvm.Name} has been changed..)" });
        }
        return View();
    }
}