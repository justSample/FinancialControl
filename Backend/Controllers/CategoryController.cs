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
    public async Task<IActionResult> Get(Guid id)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        var categories = await context.Categories.Where(x => x.IdUser == id).ToArrayAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CategoryModel category)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        if (await context.Categories.AnyAsync(c => c.IdUser == category.IdUser && c.Name == category.Name))
            return BadRequest("Категория с таким названием уже есть");

        Category newCategory = new Category()
        {
            Name = category.Name,
            IdUser = category.IdUser,
            IdParent = category.IdParent,
        };

        _ = await context.Categories.AddAsync(newCategory);
        _ = await context.SaveChangesAsync(); // Пока игнорируем, но можно будет в лог выводить

        return StatusCode(201, newCategory);
    }

    [HttpPut("{idCategory}")]
    public async Task<IActionResult> Put(int idCategory, [FromBody] CategoryModel category)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        bool hasSomeCategory = await context.Categories
            .AnyAsync(c => c.IdUser == category.IdUser && c.Name == category.Name);
        
        if (hasSomeCategory) return BadRequest("Такая категория у вас уже есть!");
        
        var foundCategory = await context.Categories
            .FirstOrDefaultAsync(c => c.IdUser == category.IdUser && c.Id == idCategory);
        
        if (foundCategory == null) return NotFound("Категория не найдена!");
        
        foundCategory.Name = category.Name;
        foundCategory.IdParent = category.IdParent;
        foundCategory.IdUser = category.IdUser;
        
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{idUser}/{idCategory}")]
    public async Task<IActionResult> Delete(Guid idUser, int idCategory)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var foundCategory = await context.Categories.FirstOrDefaultAsync(c => c.IdUser == idUser && c.Id == idCategory);

        if (foundCategory == null) return NotFound("Категория не найдена!");

        context.Categories.Remove(foundCategory);
        _ = await context.SaveChangesAsync();

        return NoContent();
    }
}