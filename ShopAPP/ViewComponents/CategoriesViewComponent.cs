using Microsoft.AspNetCore.Mvc;
using ShopAPP.Models;
using ShopAPP.Service.Layer.Services.CategoryService;

namespace ShopAPP.Components;
public class CategoriesViewComponent:ViewComponent
{
	private ICategoryService _categoryService;

    public CategoriesViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public IViewComponentResult Invoke() 
	{
		if (RouteData.Values["category"]!=null)
			ViewBag.SelectedCategory = RouteData?.Values["category"];
		return View(_categoryService.GetAll());
	}
}
