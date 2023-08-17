using App.Core.Abstract;

namespace Asp_8.Entites;

public class Theme : BaseEntity, IEntity
{
    public List<Books>? Books { get; set; }
}
