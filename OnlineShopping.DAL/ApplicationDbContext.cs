using Microsoft.EntityFrameworkCore;
using OnlineShopping.Domain.Entity;

namespace OnlineShopping.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } 
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}