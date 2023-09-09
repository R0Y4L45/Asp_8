using Asp_8.Entites;

namespace BookStore.WebUI.Models;

public class CategoryViewModel
{
    public IEnumerable<Category>? Categories { get; set; }
    public int CurrentCategory { get; set; }
}
