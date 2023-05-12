namespace OnlineShopping.Domain.Entity;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}