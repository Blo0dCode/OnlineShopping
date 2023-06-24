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
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Category> _categoryRepository;

    public ProductService(IBaseRepository<Product> productRepository, IBaseRepository<Category> categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IBaseResponse<List<Product>>> GetProductsAsync()
    {
        try
        {
            var products = await _productRepository.GetAll().Include(x => x.Category).ToListAsync();
            if (!products.Any())
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = "Продукты не найдены",
                    StatusCode = StatusCode.ProductsNotFound
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
    } 

    public async Task<IBaseResponse<Product>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await _productRepository.GetAll().Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Продукт не найден",
                    StatusCode = StatusCode.ProductNotFound
                };
            }

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
                Description = $"[GetProductByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    } 

    public async Task<IBaseResponse<Product>> CreateProductAsync(ProductViewModel productViewModel, byte[] imageData)
    {
        try
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == productViewModel.CategoryId);
            if (category == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }
            
            var product = _mapper.ToProduct(productViewModel, category, imageData);

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
    } 

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
                    Description = "Продукт не найден",
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
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == productViewModel.CategoryId);
            
            if (product == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Продукт не найден",
                    StatusCode = StatusCode.ProductNotFound
                };
            }
            if (category == null)
            {
                return new BaseResponse<Product>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }

            product.Id = productViewModel.Id;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Category = category;
            product.Price = productViewModel.Price;

            await _productRepository.Update(product);

            return new BaseResponse<Product>()
            {
                Data = product,
                StatusCode = StatusCode.Updated
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
    } 

    public async Task<IBaseResponse<List<ProductViewModel>>> GetProductsByCategoryIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return new BaseResponse<List<ProductViewModel>>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }

            var products = await _productRepository.GetAll().Where(x => x.Category.Id == id).ToListAsync();
            if (!products.Any())
            {
                return new BaseResponse<List<ProductViewModel>>()
                {
                    Description = "Продукты не найдены",
                    StatusCode = StatusCode.ProductsNotFound
                };
            }

            var productsViewModel = products.Select(x => _mapper.ToProductViewModel(x)).ToList();

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
    } 
}