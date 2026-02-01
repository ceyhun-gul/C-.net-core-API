using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(int categoryId)
    {
        return context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public IQueryable<Category> GetCategoryWithProducts()
    {
        return context.Categories.Include(x => x.Products).AsQueryable();
    }
}