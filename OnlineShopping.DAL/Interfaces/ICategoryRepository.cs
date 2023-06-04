using OnlineShopping.Domain.Entity;

namespace OnlineShopping.DAL.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    IQueryable<Category> GetCategoryByIdAsync(int? categoryId);
}