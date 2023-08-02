namespace Asp_8.Models;

public class Book_messageVM
{
    public IQueryable<BookViewModel>? BookViewModels { get; set; }
    public string? Message { get; set; }
}
