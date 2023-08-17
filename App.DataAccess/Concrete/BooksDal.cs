using App.Core.DataAccess.EntityFramework;
using App.DataAccess.Abstract;
using Asp_8.DataBaseContext;
using Asp_8.Entites;

namespace App.DataAccess.Concrete;

public class BooksDal : EfEntityRepositoryBase<Books, BookStoreDbContext>, IBooksDal
{
}
