using System.ComponentModel;

namespace Asp_8.Models;

public class BookViewModel
{
    [DisplayName("Book Name")]
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Count { get; set; }
    public string? Description { get; set; }
    public string Press { get; set; } = null!;
    public string? Theme { get; set; }
    public string Category { get; set; } = null!;

    [DisplayName("Author Name")]
    public string AuthorName { get; set; } = null!;
    [DisplayName("Author Surname")]
    public string AuthorSurname { get; set; } = null!;

}
