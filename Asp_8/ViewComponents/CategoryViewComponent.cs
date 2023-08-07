using Asp_8.Areas.User.Models;
using Asp_8.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Asp_8.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    private readonly BookStoreDbContext _dbContext;
    public CategoryViewComponent(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ViewViewComponentResult Invoke() => View(new CategoryViewModel { Categories = _dbContext?.Categories?.ToList() }); 
}
