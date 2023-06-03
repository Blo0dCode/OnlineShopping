using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Category;

namespace OnlineShopping.Service.Interfaces;

public interface ICategoryService
{
    IBaseResponse<List<Category>> GetCategories();
    Task<IBaseResponse<CategoryViewModel>> GetCategoryByIdAsync(int categoryId);
    Task<IBaseResponse<Category>> CreateCategoryAsync(CategoryViewModel categoryViewModel);
    Task<IBaseResponse<bool>> DeleteCategoryByIdAsync(int id);
}