namespace App.Entities.Entity;
public class Cart
{
    public List<CartLine> CartLines { get; set; }
    public Cart()
    {
        CartLines = new List<CartLine>();
    }
    public decimal Total
    {
        get => CartLines.Sum(c => c.Book!.Price * c.Quantity);
    }
}
