using App.Business.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;
using System.Linq.Expressions;

namespace App.Business.Concrete;

public class CategoryService : ICategoryService
{
    public BookStoreDbContext Context { get; }

    public CategoryService()
    {
        Context = new BookStoreDbContext();
    }

    public void Add(Category entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Delete(Category entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public Category Get(Expression<Func<Category, bool>> filter = null!) => Context.Set<Category>().FirstOrDefault(filter)!;
    public IEnumerable<Category> GetList(Expression<Func<Category, bool>> filter = null!) =>
        filter == null ? Context.Set<Category>() : Context.Set<Category>().Where(filter);
    public bool Update(Category entity)
    {
        if (Context.Categories?.FirstOrDefault(c => c.Id == entity.Id && c.Name == entity.Name) is null)
        {
            Category c = Context.Categories?.FirstOrDefault(c => c.Id == entity.Id)!;
            c.Name = entity.Name;
            Context.SaveChanges();
            return true;
        }
        return false;
    }
}
