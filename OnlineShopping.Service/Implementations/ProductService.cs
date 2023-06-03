using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Enum;
using OnlineShopping.Domain.Interface;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Product;
using OnlineShopping.Service.Interfaces;

namespace OnlineShopping.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public IBaseResponse<List<Product>> GetProductsAsync()
    {
        try
        {
            var products = _productRepository.GetAll().ToList(); //TODO async?
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
                Description = $"[GetProductsAsync] : {e.Message}",
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

            var data = _mapper.ToProductViewModel(product);

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
                Description = $"[GetProductByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<Product>> CreateProductAsync(ProductViewModel productViewModel, byte[] imageData)
    {
        try
        {
            var category = _categoryRepository.GetCategoryByIdAsync(productViewModel.CategoryId);
            var product = _mapper.ToProduct(productViewModel, category.FirstOrDefault(), imageData); //TODO правильно?

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
                Description = $"[CreateProductAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<bool>> DeleteProductByIdAsync(int id)
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
                StatusCode = StatusCode.Deleted
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteProductByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public async Task<IBaseResponse<Product>> UpdateProductAsync(ProductViewModel productViewModel)
    {
        try
        {
            var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == productViewModel.Id);
            var category = _categoryRepository.GetCategoryByIdAsync(productViewModel.CategoryId);
            if (product == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Element not found",
                    StatusCode = StatusCode.NoContent
                };
            }

            product.Id = productViewModel.Id;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Category = (Category)category;
            product.Price = productViewModel.Price;

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
                Description = $"[UpdateProductAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //

    public IBaseResponse<List<ProductViewModel>> GetProductsByCategoryIdAsync(int id)
    {
        try
        {
            var category = _categoryRepository.GetCategoryByIdAsync(id);
            if (!category.Any())
            {
                return new BaseResponse<List<ProductViewModel>>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }
            
            var products = _productRepository.GetProductsByCategoryId(id).ToList(); // TODO async?
            if (!products.Any())
            {
                return new BaseResponse<List<ProductViewModel>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.NoContent
                };
            }

            var productsViewModel = products.Select(x => _mapper.ToProductViewModel(x)).ToList(); //TODO async?

            return new BaseResponse<List<ProductViewModel>>()
            {
                Data = productsViewModel,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<ProductViewModel>>()
            {
                Description = $"[GetProductsByCategoryIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } //
}