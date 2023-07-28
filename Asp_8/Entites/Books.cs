using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp_8.Entites;

public class Books : BaseEntity
{
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public int Count { get; set; }
    public string? Description { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int ThemeId { get; set; }
    public Theme? Theme { get; set; }

    public int PressId { get; set; }
    public Press? Press { get; set; }

    public List<AuthorBooks>? AuthorBooks { get; set; }
}
