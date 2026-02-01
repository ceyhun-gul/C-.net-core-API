using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.ExceptionHandler;

namespace Services;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        // manuel mapper -> en hizli calisir
        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();
        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();

        #region manuel mapping

        // var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();

        #endregion

        var productsAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllList(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take((pageSize))
            .ToListAsync();

        #region manuel mapping

        // var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();

        #endregion

        var productsAsDto = mapper.Map<List<ProductDto>>(products);


        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        #region manuel mapping

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Stock, product.Price);

        #endregion

        var productsAsDto = mapper.Map<ProductDto>(product);


        return ServiceResult<ProductDto>.Success(productAsDto!, HttpStatusCode.OK);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        // throw new Exception("Exception");
        var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Product not found", HttpStatusCode.BadRequest);
        }

        var product = new Product()
        {
            Name = request.Name,
            Stock = request.Stock,
            Price = request.Price
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),
            $"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        var isProductNameExist =
            await productRepository.Where(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();

        if (isProductNameExist)
        {
            return ServiceResult.Fail("Product already in DB", HttpStatusCode.NotFound);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.productId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Stock = request.quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(DeleteProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}