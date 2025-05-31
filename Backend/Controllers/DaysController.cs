using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("days")]
public class DaysController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await using FinancialDbContext context = new FinancialDbContext();
        return Ok();
    }
    
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