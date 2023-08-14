using Asp_8.Context;
using BookStore.WebUI.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BookStore.WebUI.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    private readonly BookStoreDbContext _dbContext;
    public CategoryViewComponent(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ViewViewComponentResult Invoke() => View(new CategoryViewModel { Categories = _dbContext?.Categories?.ToList() });
}
