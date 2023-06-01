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
    public IActionResult GetProducts(int id)
    {
        var response = _productService.GetProductsAsync();
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

    [HttpGet]
    public IActionResult GetProductsByCategoryId(int id)
    {
        var response = _productService.GetProductsByCategoryIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View("Error");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.DeleteProductByIdAsync(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Deleted)
        {
            return RedirectToAction("GetProducts");
        }

        //return View("Error", $"{response.Description}");
        return View("Error");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
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
        }

        return View("Error");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProductAsync(viewModel);
        }
        return RedirectToAction("GetProducts");
    }
}