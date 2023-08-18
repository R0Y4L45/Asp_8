using App.Business.Abstract;
using BookStore.WebUI.Areas.User.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BookStore.WebUI.ViewComponents;

public class CategoryViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoryViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public ViewViewComponentResult Invoke() =>
        View(new CategoryViewModel
        {
            Categories = _categoryService.GetList(),
            CurrentCategory = Convert.ToInt32(HttpContext.Request.Query["category"])
        });
}
