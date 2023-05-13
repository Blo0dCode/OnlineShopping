using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.Interface;
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
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price
        };
    }

    public Product ToProduct(ProductViewModel productViewModel)
    {
        return new Product()
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            CategoryId = productViewModel.CategoryId,
            Description = productViewModel.Description,
            Price = productViewModel.Price
        };
    }
}