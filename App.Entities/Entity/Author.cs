using App.Core.Abstract;

namespace Asp_8.Entites;

public class Author : BaseEntity, IEntity
{
    public string Surname { get; set; } = null!;
    public List<Books>? Book { get; set; }
}
