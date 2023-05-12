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
    public IActionResult GetProducts()
    {
        var response = _productService.GetProductsAsync();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error", $"{response.Description}");
    }
}