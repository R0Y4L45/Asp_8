using System.ComponentModel.DataAnnotations;

namespace Asp_8.Entites;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
