namespace Repositories.Categories;

public class Category : IBaseAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Product>? Products { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }
}