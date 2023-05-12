using OnlineShopping.Domain.Entity;

namespace OnlineShopping.DAL.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    IQueryable<Product> GetProductsByCategoryId(int categoryId);
}