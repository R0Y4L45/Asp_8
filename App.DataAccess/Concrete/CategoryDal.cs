using App.DataAccess.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;

namespace App.DataAccess.Concrete;

public class CategoryDal : EfEntityRepositoryBase<Category, BookStoreDbContext>, ICategoryDal
{
}