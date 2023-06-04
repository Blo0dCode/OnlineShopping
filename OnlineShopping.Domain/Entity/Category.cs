namespace OnlineShopping.Domain.Entity;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Category? CategoryParent { get; set; }
    
    
    public List<Product> Products { get; set; }
}