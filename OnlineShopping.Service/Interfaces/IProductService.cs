using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Product;

namespace OnlineShopping.Service.Interfaces;

public interface IProductService
{
    Task<IBaseResponse<List<Product>>> GetProductsAsync();
    Task<IBaseResponse<Product>> GetProductByIdAsync(int id);
    Task<IBaseResponse<Product>> CreateProductAsync(ProductViewModel model, byte[] imageData);
    Task<IBaseResponse<bool>> DeleteProductByIdAsync(int id);
    Task<IBaseResponse<Product>> UpdateProductAsync(ProductViewModel productViewModel);

    Task<IBaseResponse<List<ProductViewModel>>> GetProductsByCategoryIdAsync(int id);
}