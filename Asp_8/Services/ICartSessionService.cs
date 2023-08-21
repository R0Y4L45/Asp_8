using App.Entities.Entity;

namespace BookStore.WebUI.Services;
public interface ICartSessionService
{
    Cart GetCart();
    void SetCart(Cart cart);
}
