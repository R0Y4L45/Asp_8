using App.Business.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;
using System.Linq.Expressions;

namespace App.Business.Concrete;

public class PressService : IPressService
{
    public BookStoreDbContext Context { get; }

    public PressService()
    {
        Context = new BookStoreDbContext();
    }

    public void Add(Press entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Press entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public Press Get(Expression<Func<Press, bool>> filter = null!) => Context.Set<Press>().FirstOrDefault(filter)!;
    public IEnumerable<Press> GetList(Expression<Func<Press, bool>> filter = null!) =>
        filter == null ? Context.Set<Press>().ToList() : Context.Set<Press>().Where(filter).ToList();
    public bool Update(Press entity)
    {
        if (Context.Presses?.FirstOrDefault(p => p.Id == entity.Id && p.Name == entity.Name) is null)
        {
            Press p = Context.Presses?.FirstOrDefault(p => p.Id == entity.Id)!;
            p.Name = entity.Name;
            Context.SaveChanges();
            return true;
        }
        return false;
    }
}
