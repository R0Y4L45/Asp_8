using App.Core.Abstract;

namespace Asp_8.Entites;

public class Press : BaseEntity, IEntity
{
    public List<Books>? Books { get; set; }
}
