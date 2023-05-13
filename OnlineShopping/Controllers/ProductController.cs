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
    public IActionResult GetProducts(string category)
    {
        var responsee = _productService.GetProductsAsync();
        var response = _productService.GetProductsByCategoryIdAsync(1);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        //return View("Error", $"{response.Description}");
        return View("Error");
    }

    [HttpGet]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        //return View("Error", $"{response.Description}");
        return View("Error");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteProductByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Created)
        {
            return RedirectToAction("GetProducts");
        }

        //return View("Error", $"{response.Description}");
        return View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel viewModel) // TODO через постман как оправить валидную модель?
    {
        if (!ModelState.IsValid)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
            }

            await _productService.CreateProductAsync(viewModel, imageData);
        }

        return RedirectToAction("GetProducts");
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProductAsync(viewModel);
        }

        return RedirectToAction("GetProducts");
    }
}