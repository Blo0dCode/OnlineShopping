using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers;

public class CategoryController : Controller
{
    /*private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;

    public CategoryController(ICategoryService categoryService, IProductService productService)
    {
        _categoryService = categoryService;
        _productService = productService;
    }

    [HttpGet]
    public IActionResult Index(int id)
    {
        // Получаем категорию по ее идентификатору
        var category = _categoryService.GetCategoryById(id);

        if (category == null)
        {
            // Если категория не найдена, возвращаем страницу ошибки 404
            return HttpNotFound();
        }

        // Получаем список продуктов, которые относятся к данной категории
        var products = _productService.GetProductsByCategoryId(id);

        return View(products);
    }*/
}