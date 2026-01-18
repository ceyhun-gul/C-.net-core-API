namespace Services;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);

    Task<ServiceResult<ProductDto>> GetByIdAsync(int id);

    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);

    Task<ServiceResult> UpdateAsync(UpdateProductRequest request);

    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);

    Task<ServiceResult<List<ProductDto>>> GetPagedAllList(int pageNumber, int pageSize);

    Task<ServiceResult> DeleteAsync(DeleteProductRequest request);

    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
}