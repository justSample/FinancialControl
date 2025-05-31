using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("days")]
public class DaysController : ControllerBase
{
    // В будущем по хорошему нужно переделать и разделить отдельно на дни и на финансовые операции
    [HttpGet]
    public async Task<IActionResult> Get(Guid idUser)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var allDays = await context.Days.Where(x => x.IdUser == idUser).ToArrayAsync();
        
        return Ok(allDays);
    }
    
    // Оставим на потом
    
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        await using FinancialDbContext context = new FinancialDbContext();
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put()
    {
        await using FinancialDbContext context = new FinancialDbContext();
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        await using FinancialDbContext context = new FinancialDbContext();
        return Ok();
    }
}