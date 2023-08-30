using BookStore.WebUI.Areas.User.Models;
using BookStore.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BookStore.WebUI.ViewComponents;
public class CartSummaryViewComponent : ViewComponent
{
    private ICartSessionService _sessionService;

    public CartSummaryViewComponent(ICartSessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public ViewViewComponentResult Invoke() =>
        View(new CartSummaryViewModel
        {
            Cart = _sessionService.GetCart()
        });
}
