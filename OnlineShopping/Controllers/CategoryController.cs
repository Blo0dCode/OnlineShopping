using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Domain.ViewModels.Category;
using OnlineShopping.Service.Interfaces;

namespace OnlineShopping.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService; 
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var response = await _categoryService.GetCategoriesAsync();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var response = await _categoryService.GetCategoryByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel categoryViewModel)
    {
        var response = await _categoryService.CreateCategoryAsync(categoryViewModel);

        if (response.StatusCode == Domain.Enum.StatusCode.Created)
        {
            return RedirectToAction("GetCategories");
        }

        return View("Error", $"{response.Description}");
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody]CategoryViewModel viewModel)
    {
        //if (ModelState.IsValid)

        var response = await _categoryService.UpdateCategoryAsync(viewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.Updated)
        {
            return RedirectToAction("GetCategories");
        }

        return View("Error", $"{response.Description}");
    }

    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _categoryService.DeleteCategoryByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Deleted)
        {
            return RedirectToAction("GetCategories");
        }

        return View("Error", $"{response.Description}");
    }
}