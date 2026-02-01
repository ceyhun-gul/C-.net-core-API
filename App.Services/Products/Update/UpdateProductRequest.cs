namespace Services;

public record UpdateProductRequest(int Id, string Name, decimal Price, int Stock,int CategoryId);