using App.Core.DataAccess;
using Asp_8.DataBaseContext;
using Asp_8.Entites;

namespace App.Business.Abstract;

public interface IAuthorService : IEntityRepository<Author, BookStoreDbContext>
{
}
