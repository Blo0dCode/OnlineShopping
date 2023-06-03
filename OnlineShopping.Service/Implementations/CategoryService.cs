using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Enum;
using OnlineShopping.Domain.Interface;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Category;
using OnlineShopping.Domain.ViewModels.Product;
using OnlineShopping.Service.Interfaces;

namespace OnlineShopping.Service.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public IBaseResponse<List<Category>> GetCategories()
    {
        try
        {
            var categories = _categoryRepository.GetAll().ToList(); //TODO async?
            if (!categories.Any())
            {
                return new BaseResponse<List<Category>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.NoContent
                };
            }

            return new BaseResponse<List<Category>>()
            {
                Data = categories,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Category>>()
            {
                Description = $"[GetProductsAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<CategoryViewModel>> GetCategoryByIdAsync(int categoryId)
    {
        try
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category == null)
            {
                return new BaseResponse<CategoryViewModel>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }

            var data = _mapper.ToCategoryViewModel(category);

            return new BaseResponse<CategoryViewModel>()
            {
                Data = data,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<CategoryViewModel>()
            {
                Description = $"[GetProductByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Category>> CreateCategoryAsync(CategoryViewModel categoryViewModel)
    {
        try
        {
            Category category;
            
            if (categoryViewModel.CategoryParentId == null)
            {
                category = _mapper.ToCategory(categoryViewModel);
            }
            else
            {
                var categoryParent = _categoryRepository.GetCategoryByIdAsync(categoryViewModel.CategoryParentId);
                category = _mapper.ToCategory(categoryViewModel, categoryParent.FirstOrDefault()); //TODO (Category)categoryParent правильно?
            }

            await _categoryRepository.Create(category);

            return new BaseResponse<Category>()
            {
                Data = category,
                StatusCode = StatusCode.Created
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Category>()
            {
                Description = $"[CreateCategoryAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteCategoryByIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Element not found",
                    StatusCode = StatusCode.NoContent
                };
            }

            await _categoryRepository.Delete(category);

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
                Description = $"[DeleteCategoryByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}