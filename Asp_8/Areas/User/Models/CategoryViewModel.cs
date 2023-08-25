using Asp_8.Entites;

namespace BookStore.WebUI.Areas.User.Models;

public class CategoryViewModel
{
    public IEnumerable<Category>? Categories { get; set; }
    public int CurrentCategory { get; set; }
}
