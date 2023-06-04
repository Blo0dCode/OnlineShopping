using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Enum;
using OnlineShopping.Domain.Interface;
using OnlineShopping.Domain.Response;
using OnlineShopping.Domain.ViewModels.Category;
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

    public async Task<IBaseResponse<List<Category>>> GetCategoriesAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();
            if (!categories.Any())
            {
                return new BaseResponse<List<Category>>()
                {
                    Description = "Категории не найдены",
                    StatusCode = StatusCode.CategoriesNotFound
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
                Description = $"[GetCategoriesAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Category>> GetCategoryByIdAsync(int categoryId)
    {
        try
        {
            var category = await _categoryRepository.GetAll().FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category == null)
            {
                return new BaseResponse<Category>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }

            return new BaseResponse<Category>()
            {
                Data = category,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Category>()
            {
                Description = $"[GetCategoryByIdAsync] : {e.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<Category>> CreateCategoryAsync(CategoryViewModel categoryViewModel)
    {
        try
        {
            var categoryParent = await _categoryRepository.GetCategoryByIdAsync(categoryViewModel.CategoryParentId).FirstOrDefaultAsync();
            if (categoryViewModel.CategoryParentId != 0 && categoryParent == null)
            {
                return new BaseResponse<Category>()
                {
                    Description = "Родительская категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }
            var category = _mapper.ToCategory(categoryViewModel, categoryParent);

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

    public async Task<IBaseResponse<Category>> UpdateCategoryAsync(CategoryViewModel categoryViewModel)
    {
        try
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryViewModel.Id).Include(x=>x.CategoryParent).FirstOrDefaultAsync();
            var categoryParent = await _categoryRepository.GetCategoryByIdAsync(categoryViewModel.CategoryParentId)
                .FirstOrDefaultAsync();
            
            if (category == null && categoryViewModel.CategoryParentId !=0 || categoryParent == null)
            {
                return new BaseResponse<Category>()
                {
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
                };
            }

            category.Id = categoryViewModel.Id;
            category.Name = categoryViewModel.Name;
            category.CategoryParent = categoryParent;

            await _categoryRepository.Update(category);

            return new BaseResponse<Category>()
            {
                Data = category,
                StatusCode = StatusCode.Updated
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Category>()
            {
                Description = $"[UpdateProductAsync] : {e.Message}",
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
                    Description = "Категория не найдена",
                    StatusCode = StatusCode.CategoryNotFound
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