using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.ViewModels.Category;
using OnlineShopping.Domain.ViewModels.Product;

namespace OnlineShopping.Domain.Interface;

public interface IMapper
{
    //Product
    ProductViewModel ToProductViewModel(Product product);
    Product ToProduct(ProductViewModel productViewModel, Category category, byte[] imageData);

    
    //Category
    Category ToCategory(CategoryViewModel categoryViewModel, Category categoryParent);
    Category ToCategory(CategoryViewModel categoryViewModel);
    CategoryViewModel ToCategoryViewModel(Category category);
}