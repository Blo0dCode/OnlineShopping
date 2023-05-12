using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Product;

namespace OnlineShopping.Service.Interfaces;

public interface IProductService
{
    IBaseResponse<List<Product>> GetProductsAsync();
    Task<IBaseResponse<ProductViewModel>> GetProductByIdAsync(int id);
    Task<IBaseResponse<Product>> CreateProductAsync(ProductViewModel model, byte[] imageData);
    Task<IBaseResponse<bool>> DeleteProductAsync(int id);
    Task<IBaseResponse<Product>> UpdateProductAsync(int id, ProductViewModel model);

    IBaseResponse<List<Product>> GetProductsByCategoryIdAsync(int id, Product entity);
}