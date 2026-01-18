using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace App.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController

{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) =>
        CreateActionResult(await productService.GetPagedAllList(pageNumber, pageSize));


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) =>
        CreateActionResult(await productService.CreateAsync(request));

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductRequest request) =>
        CreateActionResult(await productService.UpdateAsync(request));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) =>
        CreateActionResult(await productService.UpdateStockAsync(request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, DeleteProductRequest request) =>
        CreateActionResult(await productService.DeleteAsync(request));
}