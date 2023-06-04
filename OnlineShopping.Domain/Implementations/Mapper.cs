using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Interface;
using OnlineShopping.Domain.ViewModels.Category;
using OnlineShopping.Domain.ViewModels.Product;

namespace OnlineShopping.Domain.Implementations;

public class Mapper : IMapper
{
    public ProductViewModel ToProductViewModel(Product product)
    {
        return new ProductViewModel()
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.Category.Id,
            Description = product.Description,
            Price = product.Price
        };
    }

    public Product ToProduct(ProductViewModel productViewModel, Category category, byte[] imageData)
    {
        return new Product()
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            Category = category,
            Description = productViewModel.Description,
            Price = productViewModel.Price,
            Avatar = imageData
        };
    }

    public Category ToCategory(CategoryViewModel categoryViewModel, Category categoryParent)
    {
        return new Category()
        {
            Id = categoryViewModel.Id,
            Name = categoryViewModel.Name,
            CategoryParent = categoryParent
        };
    }

    public CategoryViewModel ToCategoryViewModel(Category category)
    {
        if (category.CategoryParent!=null)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name, 
                CategoryParentId = category.CategoryParent.Id
            };
        }
        else
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
        
    }
}