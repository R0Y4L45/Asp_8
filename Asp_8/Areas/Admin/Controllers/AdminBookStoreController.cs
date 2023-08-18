using App.Business.Abstract;
using Asp_8.Entites;
using BookStore.WebUI.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminBookStoreController : Controller
{
    private bool _flag = false;
    private readonly ICategoryService? _c;
    private readonly IAuthorService? _a;
    private readonly IPressService? _p;
    private readonly IBooksService? _b;
    private readonly IThemeService? _t;

    public AdminBookStoreController(ICategoryService? c, IAuthorService? a, IPressService? p, IBooksService? b, IThemeService? t)
    {
        _c = c;
        _a = a;
        _p = p;
        _b = b;
        _t = t;

        //_c?.Add(new Category { Name = "SQL Language" });
        //_c?.Add(new Category { Name = "C++ Builder" });
        //_c?.Add(new Category { Name = "Delphi" });
        //_c?.Add(new Category { Name = "Visual Basic" });
        //_c?.Add(new Category { Name = "3D Studio Max" });
        //_c?.Add(new Category { Name = "Mathcad" });
        //_c?.Add(new Category { Name = "Novel" });
        //_c?.Add(new Category { Name = "Mathematical Analysis" });

        //_t?.Add(new Theme { Name = "Bases of data" });
        //_t?.Add(new Theme { Name = "Graphic Packages" });
        //_t?.Add(new Theme { Name = "High Mathematics" });
        //_t?.Add(new Theme { Name = "Networks" });
        //_t?.Add(new Theme { Name = "Web - design" });
        //_t?.Add(new Theme { Name = "Windows 2000" });
        //_t?.Add(new Theme { Name = "Operating systems" });

        //_p?.Add(new Press { Name = "DiaSoft" });
        //_p?.Add(new Press { Name = "BHV" });
        //_p?.Add(new Press { Name = "Dialectics" });
        //_p?.Add(new Press { Name = "Kudits - Image" });
        //_p?.Add(new Press { Name = "Nauka" });
        //_p?.Add(new Press { Name = "Binom" });

        //_a?.Add(new Author { Name = "James", Surname = "Groff" });
        //_a?.Add(new Author { Name = "Michael", Surname = "Marows" });
        //_a?.Add(new Author { Name = "Boris", Surname = "Carpov" });
        //_a?.Add(new Author { Name = "Vladimir", Surname = "Korol" });
        //_a?.Add(new Author { Name = "Kevin", Surname = "Reichard" });
        //_a?.Add(new Author { Name = "Olga", Surname = "Kokoreva" });

        //_b?.Add(new Books { Name = "SQL", Count = 12, PressId = 1, ThemeId = 1, Description = "BestSeller", Price = 3.2M, CategoryId = 1, AuthorId = 1 });
        //_b?.Add(new Books { Name = "3D Studio Max 3", Count = 22, PressId = 2, ThemeId = 2, Description = "Sold", Price = 2.2M, CategoryId = 2, AuthorId = 2 });
        //_b?.Add(new Books { Name = "Visual Basic 6", Count = 15, PressId = 3, ThemeId = 1, Description = "BestSeller", Price = 1.8M, CategoryId = 5, AuthorId = 3 });
        //_b?.Add(new Books { Name = @"C++ Builder", Count = 14, PressId = 4, ThemeId = 4, Description = "BestSeller", Price = 3.3M, CategoryId = 4, AuthorId = 4 });
        //_b?.Add(new Books { Name = "Mathcad", Count = 21, PressId = 5, ThemeId = 3, Description = "BestSeller", Price = 4.2M, CategoryId = 3, AuthorId = 5 });
        //_b?.Add(new Books { Name = "Delphi helper", Count = 10, PressId = 3, ThemeId = 5, Description = "BestSeller", Price = 3.6M, CategoryId = 6, AuthorId = 6 });
    }

    public IActionResult Main(int page = 1, int category = 0)
    {
        int pageSize = 10;

        IEnumerable<BookViewModel> booksList;
        booksList = (from b in _b?.GetList()
                     join c in category > 0 ? _c?.GetList(c => c.Id == category)! : _c?.GetList()! on b.CategoryId equals c.Id
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

        BooksListViewModel model = new BooksListViewModel
        {
            Books = booksList.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            PageCount = (int)Math.Ceiling(booksList.Count() / (double)pageSize),
            PageSize = pageSize,
            CurrentPage = page,
            Role = true,
            CurrentCategory = category
        };

        return View(model);
    }

    public IActionResult Add() => View(new BookViewModel());

    [HttpPost]
    public IActionResult Add(BookViewModel bvm)
    {
        if (ModelState.IsValid)
        {
            int aId, cId, tId, pId;

            if (_a!.Get(a => a.Name == bvm.AuthorName && a.Surname == bvm.AuthorSurname) is Author a)
                aId = a.Id;
            else
            {
                _a.Add(new Author { Name = bvm.AuthorName, Surname = bvm.AuthorSurname });
                aId = _a.Get(a => a.Name == bvm.AuthorName && a.Surname == bvm.AuthorSurname).Id;
            }

            if (_c!.Get(c => c.Name == bvm.Category) is Category c)
                cId = c.Id;
            else
            {
                _c.Add(new Category { Name = bvm.Category });
                cId = _c.Get(c => c.Name == bvm.Category).Id;
            }

            if (_t!.Get(t => t.Name == bvm.Theme) is Theme t)
                tId = t.Id;
            else
            {
                _t.Add(new Theme { Name = bvm.Theme });
                tId = _t.Get(t => t.Name == bvm.Theme).Id;
            }

            if (_p!.Get(p => p.Name == bvm.Press) is Press p)
                pId = p.Id;
            else
            {
                _p.Add(new Press { Name = bvm.Press });
                pId = _p.Get(p => p.Name == bvm.Press).Id;
            }

            if (_b!.Get(b => b.Name == bvm.Name) is Books b && b.PressId == pId && b.ThemeId == tId && b.AuthorId == aId && b.CategoryId == cId)
                _flag = false;
            else
            {
                _b.Add(new Books
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

                _flag = true;
            }

            if (_flag) TempData["N"] = $"Book : {bvm.Name} added ;-)";
            else TempData["N"] = $"Book : {bvm.Name} has been added by YOU ;-)";

            return RedirectToAction("Main");
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        List<Books> books = _b!.GetList();
        Books? book = _b!.Get(b => b.Id == id);

        _b.Delete(book);
        var d = books.Count(b => b.AuthorId == book.AuthorId);

        if (books.Count(b => b.AuthorId == book.AuthorId) == 1)
            _a!.Delete(_a.Get(a => a.Id == book.AuthorId));
        if (books.Count(b => b.ThemeId == book.ThemeId) == 1)
            _t!.Delete(_t.Get(t => t.Id == book.ThemeId));
        if (books.Count(b => b.PressId == book.PressId) == 1)
            _p!.Delete(_p.Get(p => p.Id == book.PressId));
        if (books.Count(b => b.CategoryId == book.CategoryId) == 1)
            _c!.Delete(_c.Get(c => c.Id == book.CategoryId));

        if (!TempData.Keys.Contains("N"))
            TempData.Add("N", $"Book : {book?.Name} was deleted ;-(");
        else
            TempData["N"] = $"Book : {book?.Name} was deleted ;-(";

        return RedirectToAction("Main");
    }

    public IActionResult Edit(int id)
    {
        var bvm = (from b in _b!.GetList()
                   join c in _c!.GetList()! on b.CategoryId equals c.Id
                   join p in _p!.GetList()! on b.PressId equals p.Id
                   join t in _t!.GetList()! on b.ThemeId equals t.Id
                   join a in _a!.GetList()! on b.AuthorId equals a.Id
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
            List<bool> flags = new();
            Books book = _b!.Get(b => b.Id == bvm.BookId);

            flags.Add(_t!.Update(new Theme { Id = book.ThemeId, Name = bvm.Theme }));
            flags.Add(_c!.Update(new Category { Id = book.CategoryId, Name = bvm.Category }));
            flags.Add(_p!.Update(new Press { Id = book.PressId, Name = bvm.Press }));
            flags.Add(_a!.Update(new Author { Id = book.AuthorId, Name = bvm.AuthorName, Surname = bvm.AuthorSurname }));
            flags.Add(_b!.Update(new Books { Id = bvm.BookId, Name = bvm.Name, Count = bvm.Count, Price = bvm.Price }));

            if (!TempData.Keys.Contains("N"))
            {
                if (flags.Contains(true)) TempData.Add("N", $"Book => {bvm.Name} has been changed..)");
                else TempData.Add("N", $"Book => {bvm.Name} has not been changed..(");
            }
            else
            {
                if (flags.Contains(true)) TempData["N"] = $"Book => {bvm.Name} has been changed..)";
                else TempData["N"] = $"Book => {bvm.Name} has not been changed..(";
            }

            return RedirectToAction("Main");
        }

        return View();
    }
}