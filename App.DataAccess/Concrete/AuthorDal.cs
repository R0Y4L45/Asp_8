using App.DataAccess.Abstract;
using Asp_8.Entites;
using Asp_8.DataBaseContext;
using System.Linq.Expressions;

namespace App.DataAccess.Concrete;

public class AuthorDal : IAuthorsDal
{
    public BookStoreDbContext Context { get; } = null!;

    public AuthorDal()
    {
        Context = new BookStoreDbContext();
    }
    public void Add(Author entity)
    {
        Context?.Authors?.Add(entity);
        Context?.SaveChanges();
    }

    public void Delete(Author entity)
    {
        throw new NotImplementedException();
    }

    public Author Get(Expression<Func<Author, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public List<Author> GetList(Expression<Func<Author, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Author entity)
    {
        throw new NotImplementedException();
    }
}
