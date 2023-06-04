using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Category;

namespace OnlineShopping.Service.Interfaces;

public interface ICategoryService
{
    Task<IBaseResponse<List<Category>>> GetCategoriesAsync();
    Task<IBaseResponse<Category>> GetCategoryByIdAsync(int categoryId);
    Task<IBaseResponse<Category>> CreateCategoryAsync(CategoryViewModel categoryViewModel);
    Task<IBaseResponse<Category>> UpdateCategoryAsync(CategoryViewModel categoryViewModel);
    Task<IBaseResponse<bool>> DeleteCategoryByIdAsync(int id);
}