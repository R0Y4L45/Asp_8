using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.WebUI.Areas.User.Models;

public class BookViewModel
{
    public int BookId { get; set; }
    [DisplayName("Book Name")]
    public string Name { get; set; } = null!;

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Only positive number allowed")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public int Count { get; set; }
    public string? Description { get; set; }
    public string Press { get; set; } = null!;
    public string Theme { get; set; } = null!;
    public string Category { get; set; } = null!;

    [DisplayName("Author Name")]
    public string AuthorName { get; set; } = null!;
    [DisplayName("Author Surname")]
    public string AuthorSurname { get; set; } = null!;

}
