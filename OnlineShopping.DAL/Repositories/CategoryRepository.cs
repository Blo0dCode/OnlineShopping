using OnlineShopping.DAL.Interfaces;
using OnlineShopping.Domain.Entity;

namespace OnlineShopping.DAL.Repositories;

public class CategoryRepository : IBaseRepository<Category>
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Create(Category category)
    {
        await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();
    }

    public IQueryable<Category> GetAll()
    {
        return _db.Categories;
    }

    public async Task Delete(Category category)
    {
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }

    public async Task<Category> Update(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
        return category;
    }
}