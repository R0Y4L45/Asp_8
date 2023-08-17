using App.Business.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;
using System.Linq.Expressions;

namespace App.Business.Concrete;

public class AuthorService : IAuthorService
{
    public BookStoreDbContext Context { get; }

    public AuthorService()
    {
        Context = new BookStoreDbContext();   
    }

    public void Add(Author entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Author entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public Author Get(Expression<Func<Author, bool>> filter = null!) => Context.Set<Author>().FirstOrDefault(filter)!;
    public List<Author> GetList(Expression<Func<Author, bool>> filter = null!) => 
        filter == null ? Context.Set<Author>().ToList() : Context.Set<Author>().Where(filter).ToList();

    public bool Update(Author entity)
    {
        if (Context.Authors?.FirstOrDefault(a => a.Id == entity.Id && a.Name == entity.Name && a.Surname == entity.Surname) is null)
        {
            Author a = Context.Authors?.FirstOrDefault(a => a.Id == entity.Id)!;
            a.Name = entity.Name;
            a.Surname = entity.Surname;
            Context.SaveChanges();
            return true;
        }
        return false;
    }
}
