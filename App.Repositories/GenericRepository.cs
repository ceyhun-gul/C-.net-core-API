using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
{
    protected AppDbContext Context = context;

    private DbSet<T> DbSet => context.Set<T>();

    public IQueryable<T> GetAll() => DbSet.AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) =>
        DbSet.Where(predicate).AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(object id) => DbSet.FindAsync(id);

    public async ValueTask AddAsync(T entity) => await DbSet.AddAsync(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);
}