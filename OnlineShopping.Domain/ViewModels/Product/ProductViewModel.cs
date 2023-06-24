using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Http;

namespace OnlineShopping.Domain.ViewModels.Product;

public class ProductViewModel
{
    [Required]
    public int Id { get; set; }

    [Display(Name = "Название")]
    [Required(ErrorMessage = "Введите название")]
    [MinLength(2, ErrorMessage = " Минимальная длина должна быть больше двух символов")]
    public string Name { get; set; }

    [Display(Name = "Описание")]
    [MinLength(10, ErrorMessage = " Минимальная длина должна быть больше 10 символов")]
    public string Description { get; set; }

    [Display(Name = "Стоиомсть")]
    [Required(ErrorMessage = "Укажите стоимость")]
    public decimal Price { get; set; }
    
    [Display(Name = "Категория одежды")]
    [Required(ErrorMessage = "Введите категорию одежды")]
    public int CategoryId { get; set; }
    
    [Display(Name= "Фото")]
    [Required(ErrorMessage = "Добавте фотографию")]
    public IFormFile Avatar { get; set; }
}