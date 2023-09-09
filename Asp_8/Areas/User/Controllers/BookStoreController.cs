using Microsoft.AspNetCore.Mvc;
using BookStore.WebUI.Models;
using App.Business.Abstract;
using BookStore.WebUI.Services;
using Asp_8.Entites;
using App.Entities.Entity;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.WebUI.Areas.User.Controllers;

[Area("User")]
[Authorize(Roles = "User, Admin")]
public class BookStoreController : Controller
{
    private readonly ICategoryService? _c;
    private readonly IAuthorService? _a;
    private readonly IPressService? _p;
    private readonly IBooksService? _b;
    private readonly IThemeService? _t;
    private readonly ICartSessionService? _cart;
    private readonly ICartService? _cartService;

    public BookStoreController(ICategoryService? c, IAuthorService? a, IPressService? p, IBooksService? b, IThemeService? t, ICartSessionService? cart, ICartService? cartService)
    {
        _c = c;
        _a = a;
        _p = p;
        _b = b;
        _t = t;
        _cart = cart;
        _cartService = cartService;
    }

    public IActionResult Main(int page = 1, int category = 0)
    {
        int pageSize = 10, categoryCount = Convert.ToInt32(_c?.Context.Categories?.Count());

        IEnumerable<BookViewModel> booksList;
        IEnumerable<Category>? categoryList = _c?.GetList();

        BooksListViewModel model = new BooksListViewModel
        {
            Role = false
        };

        if (categoryList?.Count() > 0)
            model.CurrentCategory = categoryList?.Where(c => c.Id == category).Count() > 0 ? category : category >= categoryCount ? categoryList!.Last().Id : category < 0 ? 1 : 0;

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

        StaticPageSaver.Page = model.CurrentPage;
        StaticPageSaver.Category = model.CurrentCategory;

        return View(model);
    }

    public IActionResult Remove(int id)
    {
        Books? b = _b?.Get(b => b.Id == id);
        Cart? c = _cart?.GetCart();

        if (_cartService!.Contains(c!, b!))
        {
            CartLine cartLine = c?.CartLines.FirstOrDefault(b => b.Book?.Id == id)!;
            c!.Total -= cartLine.Quantity * b!.Price;

            _cartService?.RemoveFromCart(c!, id);
            _cart?.SetCart(c);

            Console.WriteLine("Total => " + c!.Total);

            if (_cartService!.Contains(c, b))
                Console.WriteLine("Yes");

            if (TempData.Keys.Contains("message"))
                TempData["message"] = $"Your product, {b?.Name} was removed successfully from cart..!";
            else
                TempData.Add("message", $"Your product, {b?.Name} was removed successfully from cart..!");
        }

        return RedirectToAction("Main", new { area = "User", page = StaticPageSaver.Page, category = StaticPageSaver.Category });
    }

    public IActionResult Buy(int Id)
    {
        Books? b = _b?.Get(b => b.Id == Id);
        Cart? c = _cart?.GetCart();

        if (!_cartService!.Contains(c!, b!))
        {
            c!.Total += b!.Price;

            _cartService?.AddToCart(c!, b!);
            _cart?.SetCart(c!);

            Console.WriteLine("Total => " + c!.Total);

            if (TempData.Keys.Contains("message"))
                TempData["message"] = $"Your product, {b?.Name} was added successfully to cart...";
            else
                TempData.Add("message", $"Your product, {b?.Name} was added successfully to cart...");
        }

        return RedirectToAction("Main", new { area = "User", page = StaticPageSaver.Page, category = StaticPageSaver.Category });
    }

    [Route("User/Plus_Minus")]
    public void Plus_Minus([FromBody] string model)
    {
        if (model != null)
        {
            string[] arr = model.Split(' ');
            Cart cart = _cart?.GetCart()!;

            if (!string.IsNullOrEmpty(arr[0]))
            {
                CartLine? cartLine = cart.CartLines.FirstOrDefault(x => x.Book!.Id == int.Parse(arr[0]));

                if (!string.IsNullOrEmpty(arr[1]))
                {
                    bool flag = int.Parse(arr[1]) > 0 ? true : false;
                    int difference;

                    if (flag)
                    {
                        difference = int.Parse(arr[1]) - cartLine!.Quantity;
                        cart.Total += difference * cartLine!.Book!.Price;
                        cartLine!.Quantity = int.Parse(arr[1]);
                    }
                    else
                    {
                        difference = cartLine!.Quantity + int.Parse(arr[1]);
                        cart.Total -= difference * cartLine!.Book!.Price;
                        cartLine!.Quantity = -1 * int.Parse(arr[1]);
                    }

                    _cart!.SetCart(cart);
                }
            }

            Console.WriteLine("Total => " + cart.Total);
        }
    }
}