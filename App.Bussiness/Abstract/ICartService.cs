using App.Entities.Entity;
using Asp_8.Entites;

namespace App.Business.Abstract;
public interface ICartService
{
    void AddToCart(Cart cart, Books book);
    void RemoveFromCart(Cart cart, int productId);
    List<CartLine> List(Cart cart);
    bool Contains(Cart cart, Books book);
}
