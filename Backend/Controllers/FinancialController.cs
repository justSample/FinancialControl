using Backend.Models;
using Backend.Models.Api;
using Backend.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("financial")]
public class FinancialController : ControllerBase
{
    
    // В будущем не забудь поставить тут skip и take когда начнёт лагать
    [HttpGet("{idUser}")]
    public async Task<IActionResult> Get(Guid idUser)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var result = await context.FinancialOperations
            .Where(x => x.IdUser == idUser)
            .ToArrayAsync();

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FinancialModel operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        DateTime selectedDate = new DateTime(
            operation.SelectedDate.Year,
            operation.SelectedDate.Month,
            operation.SelectedDate.Day);

        var foundDay = await context.Days.FirstOrDefaultAsync(x => x.Date == selectedDate);
        if (foundDay == null)
        {
            foundDay = new Day()
            {
                Date = selectedDate,
                IdUser = operation.IdUser,
                Value = 0,
            };
            await context.Days.AddAsync(foundDay);
            await context.SaveChangesAsync();
        }

        FinancialOperation newOperation = new FinancialOperation()
        {
            IdDay = foundDay.Id,
            IdUser = operation.IdUser,
            IdCategory = operation.IdCategory,
            OperationType = (FinancialOperationType)operation.OperationTypeInt,
            Value = operation.Value,
        };
        
        _ = await context.FinancialOperations.AddAsync(newOperation);
        _ = await context.SaveChangesAsync();
        
        return StatusCode(201, newOperation);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] FinancialOperation operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var foundOperation = await context.FinancialOperations
            .FirstOrDefaultAsync(x => x.Id == operation.Id && x.IdUser == operation.IdUser);
        
        if (foundOperation == null) return NotFound("Операция не найдена!");
        
        foundOperation.IdDay = operation.IdDay;
        foundOperation.IdCategory = operation.IdCategory;
        foundOperation.OperationType = operation.OperationType;
        foundOperation.Value = operation.Value;
        
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] FinancialOperation operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        
        var foundOperation = await context.FinancialOperations
            .FirstOrDefaultAsync(x => x.Id == operation.Id && x.IdUser == operation.IdUser);
        
        if (foundOperation == null) return NotFound("Операция не найдена!");
        
        context.FinancialOperations.Remove(foundOperation);
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }
}