namespace Asp_8.Entites;

public class AuthorBooks
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Books? Book { get; set; }
    
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
