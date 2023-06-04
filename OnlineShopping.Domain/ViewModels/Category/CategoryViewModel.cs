using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.Domain.ViewModels.Category;

public class CategoryViewModel
{
    [Required]
    public int Id { get; set; }
    
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Введите название")]
    [MinLength(2, ErrorMessage = " Минимальная длина должна быть больше двух символов")]
    public string Name { get; set; }
    
    public int? CategoryParentId { get; set; }
}