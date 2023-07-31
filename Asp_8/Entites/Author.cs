namespace Asp_8.Entites;

public class Author : BaseEntity
{
    public string Surname { get; set; } = null!;
    
    //Navigation_Prop
    public List<Books>? Book { get; set; }

}
