using Asp_8.Entites;

namespace App.Entities.Entity;
public class CartLine
{
    public Books? Book { get; set; }
    public int Quantity { get; set; }
}
