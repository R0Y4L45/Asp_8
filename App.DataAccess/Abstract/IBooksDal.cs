using App.Core.DataAccess;
using Asp_8.Entites;

namespace App.DataAccess.Abstract;

public interface IBooksDal : IEntityRepository<Books> 
{
}
