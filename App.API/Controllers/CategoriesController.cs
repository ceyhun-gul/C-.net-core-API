using Microsoft.AspNetCore.Mvc;
using Repositories.Categories;
using Services.Categories;
using Services.Categories.Create;
using Services.Categories.Update;

namespace App.API.Controllers;

public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id) =>
        CreateActionResult(await categoryService.GetByIdAsync(id));

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) =>
        CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts() =>
        CreateActionResult(await categoryService.GetCategoryWithProductsAsync());

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest category) =>
        CreateActionResult(await categoryService.CreateAsync(category));

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest category) =>
        CreateActionResult(await categoryService.UpdateAsync(id, category));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id) =>
        CreateActionResult(await categoryService.DeleteAsync(id));
}