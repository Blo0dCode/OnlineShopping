using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.ViewModels.Product;
using OnlineShopping.Service.Interfaces;

namespace OnlineShopping.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productService.GetProductsAsync();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategoryId(int id)
    {
        var response = await _productService.GetProductsByCategoryIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error", $"{response.Description}");
    }

    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteProductByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Deleted)
        {
            return RedirectToAction("GetProducts");
        }

        return View("Error", $"{response.Description}");
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel viewModel)
    {
        //if (ModelState.IsValid)

        byte[] imageData;
        using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
        {
            imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
        }

        var response = await _productService.CreateProductAsync(viewModel, imageData);
        if (response.StatusCode == Domain.Enum.StatusCode.Created)
        {
            return RedirectToAction("GetProducts");
        }

        return View("Error", $"{response.Description}");
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductViewModel viewModel)
    {
        //if (ModelState.IsValid)

        var response = await _productService.UpdateProductAsync(viewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.Updated)
        {
            return RedirectToAction("GetProducts");
        }

        return View("Error", $"{response.Description}");
    }
}