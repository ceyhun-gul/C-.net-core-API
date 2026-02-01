namespace Repositories.Categories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductsAsync(int categoryId);
    IQueryable<Category> GetCategoryWithProducts();
}