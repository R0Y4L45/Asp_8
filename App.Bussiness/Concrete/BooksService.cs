using App.Business.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;
using System.Linq.Expressions;

namespace App.Business.Concrete;

public class BooksService : IBooksService
{
    public BookStoreDbContext Context { get; }

    public BooksService()
    {
        Context = new BookStoreDbContext();
    }

    public void Add(Books entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Books entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public Books Get(Expression<Func<Books, bool>> filter = null!) => Context.Set<Books>().FirstOrDefault(filter)!;
    public IEnumerable<Books> GetList(Expression<Func<Books, bool>> filter = null!) =>
        filter == null ? Context.Set<Books>().ToList() : Context.Set<Books>().Where(filter).ToList();
    public bool Update(Books entity)
    {
        bool flag = false;
        Books b = Context.Books?.FirstOrDefault(b => b.Id == entity.Id)!;

        if (b.Name != entity.Name)
        {
            b.Name = entity.Name;
            flag = true;
        }

        if (b.Count != entity.Count)
        {
            b.Count = entity.Count;
            flag = true;
        }

        if (b.Price != entity.Price)
        {
            b.Price = entity.Price;
            flag = true;
        }

        if (b.Description != entity.Description)
        {
            b.Description = entity.Description;
            flag = true;
        }

        if(flag)
            Context.SaveChanges();
        
        return flag;
    }
}
