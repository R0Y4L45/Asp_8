﻿using App.Business.Abstract;
using App.Entities.Entity;
using Asp_8.Entites;

namespace App.Business.Concrete;

public class CartService : ICartService
{
    public void AddToCart(Cart cart, Books book)
    {
        CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Book?.Id == book.Id)!;
        if (cartLine == null)
            cart.CartLines.Add(new CartLine { Quantity = 1, Book = book });
    }
    public bool Contains(Cart cart, Books book) => cart.CartLines.FirstOrDefault(c => c.Book?.Id == book.Id) != null ? true : false;
    public List<CartLine> List(Cart cart) => cart.CartLines;
    public void RemoveFromCart(Cart cart, int productId) => cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Book?.Id == productId)!);
}
