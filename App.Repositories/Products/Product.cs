using Repositories.Categories;

namespace Repositories;

public class Product : IBaseAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = default!;

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }
}