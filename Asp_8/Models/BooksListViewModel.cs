namespace BookStore.WebUI.Models;

public class BooksListViewModel
{
    public IEnumerable<BookViewModel>? Books { get; set; }
    public int CurrentCategory { get; set; }
    public int PageCount { get; set; }
    public int CurrentPage { get; set; }
    public bool Role { get; set; }
}
