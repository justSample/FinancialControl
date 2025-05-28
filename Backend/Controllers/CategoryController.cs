using Backend.Models;
using Backend.Models.Api;
using Backend.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategories(Guid id)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        var categories = await context.Categories.Where(x => x.IdUser == id).ToArrayAsync();
        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryModel category)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        if (context.Categories.Any(c => c.IdUser == category.IdUser && c.Name == category.Name)) 
            return BadRequest("Категория с таким названием уже есть");
        
        Category newCategory = new Category()
        {
            Name = category.Name,
            OperationType = (FinancialOperationType) category.OperationType,
            IdUser = category.IdUser,
            IdParent = category.IdParent,
        };
        
        context.Categories.Add(newCategory);
        _ = await context.SaveChangesAsync(); // Пока игнорируем, но можно будет в лог выводить
        
        return Created();
    }
    
    [HttpPut]
    public async Task<IActionResult> EditCategory([FromBody] Category category)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var foundCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.IdUser == category.IdUser && c.Id == category.Id);
        
        if (foundCategory == null) return NotFound("Категория не найдена!");
        
        foundCategory.Name = category.Name;
        foundCategory.OperationType = category.OperationType;
        foundCategory.IdParent = category.IdParent;
        foundCategory.IdUser = category.IdUser;
        
        context.Categories.Update(foundCategory);
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromBody] Category category)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var foundCategory = await context.Categories.FirstOrDefaultAsync(c => c.IdUser == category.IdUser && c.Id == category.Id);

        if (foundCategory == null) return NotFound("Категория не найдена!");

        context.Categories.Remove(foundCategory);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }
}