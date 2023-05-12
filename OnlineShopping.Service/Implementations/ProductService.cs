using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Enum;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Product;
using OnlineShopping.Service.Interfaces;

namespace OnlineShopping.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IBaseResponse<List<Product>> GetProductsAsync()
    {
        try
        {
            var products = _productRepository.GetAll().ToList(); //TODO
            if (!products.Any())
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.NoContent
                };
            }

            return new BaseResponse<List<Product>>()
            {
                Data = products,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Product>>()
            {
                Description = $"[GetProducts] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<ProductViewModel>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = "Элемент не найден",
                    StatusCode = StatusCode.NoContent
                };
            }

            var data = new ProductViewModel()
            {
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Price = product.Price
            };

            return new BaseResponse<ProductViewModel>()
            {
                Data = data,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ProductViewModel>()
            {
                Description = $"[GetProductById] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<Product>> CreateProductAsync(ProductViewModel entity, byte[] imageData)
    {
        try
        {
            var product = new Product()
            {
                Name = entity.Name,
                CategoryId = entity.CategoryId,
                Description = entity.Description,
                Price = entity.Price
            };

            await _productRepository.Create(product);

            return new BaseResponse<Product>()
            {
                Data = product,
                StatusCode = StatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Product>()
            {
                Description = $"[Create] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Element not found",
                    StatusCode = StatusCode.NoContent
                };
            }

            await _productRepository.Delete(product);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<Product>> UpdateProductAsync(int id, ProductViewModel model)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Element not found",
                    StatusCode = StatusCode.NoContent
                };
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.Price = model.Price;

            await _productRepository.Update(product);
            
            return new BaseResponse<Product>()
            {
                Data = product,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Product>()
            {
                Description = $"[Edit] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public IBaseResponse<List<Product>> GetProductsByCategoryIdAsync(int id, Product entity) //TODO ViewModel?
    {
        try
        {
            var products = _productRepository.GetProductsByCategoryId(1).ToList();//TODO async??
            if (!products.Any())
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.NoContent
                };
            }
            
            return new BaseResponse<List<Product>>()
            {
                Data = products,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Product>>()
            {
                Description = $"[GetProductsByCategoryIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}