using OnlineShopping.Domain.Entity;
using OnlineShopping.Domain.ViewModels.Product;

namespace OnlineShopping.Domain.Interface;

public interface IMapper
{
    ProductViewModel ToProductViewModel(Product product);
    Product ToProduct(ProductViewModel productViewModel);
}