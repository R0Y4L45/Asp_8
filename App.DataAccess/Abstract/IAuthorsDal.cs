using App.Core.DataAccess;
using Asp_8.DataBaseContext;
using Asp_8.Entites;

namespace App.DataAccess.Abstract;

public interface IAuthorsDal : IEntityRepository<Author, BookStoreDbContext>
{
}
