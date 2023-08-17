using App.Core.DataAccess.EntityFramework;
using App.DataAccess.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;

namespace App.DataAccess.Concrete;

public class ThemeDal : EfEntityRepositoryBase<Theme, BookStoreDbContext>, IThemeDal
{
}
