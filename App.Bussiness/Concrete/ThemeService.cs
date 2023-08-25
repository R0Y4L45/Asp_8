using App.Business.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;
using System.Linq.Expressions;

namespace App.Business.Concrete;

public class ThemeService : IThemeService
{
    public BookStoreDbContext Context { get; }

    public ThemeService()
    {
        Context = new BookStoreDbContext();
    }

    public void Add(Theme entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Theme entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public Theme Get(Expression<Func<Theme, bool>> filter = null!) => Context.Set<Theme>().FirstOrDefault(filter)!;
    public IEnumerable<Theme> GetList(Expression<Func<Theme, bool>> filter = null!) =>
        filter == null ? Context.Set<Theme>().ToList() : Context.Set<Theme>().Where(filter).ToList();
    public bool Update(Theme entity)
    {
        if (Context.Themes?.FirstOrDefault(t => t.Id == entity.Id && t.Name == entity.Name) is null)
        {
            Theme t = Context.Themes?.FirstOrDefault(t => t.Id == entity.Id)!;
            t.Name = entity.Name;
            Context.SaveChanges();
            return true;
        }
        return false;
    }
}
