using Backend.Models;
using Backend.Models.Api;
using Backend.Models.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("financial")]
public class FinancialController : ControllerBase
{
    
    [HttpGet("{idUser}")]
    public async Task<IActionResult> GetFinancialOperations(int idUser, [FromBody] FinancialSearchModel search)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var result = await context.FinancialOperations
            .Where(x => x.IdUserAdd == idUser && x.Date >= search.StartDate && x.Date <= search.EndDate)
            .ToArrayAsync();

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddFinancialOperation([FromBody] FinancialModel operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        FinancialOperation newOperation = new FinancialOperation()
        {
            Date = DateTime.Now,
            OperationType = (FinancialOperationType) operation.OperationTypeInt,
            Value = operation.Value,
            IdCategory = operation.IdCategory,
            IdUserAdd = operation.IdUser,
        };
        
        _ = await context.FinancialOperations.AddAsync(newOperation);
        _ = await context.SaveChangesAsync();
        
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> EditFinancialOperation([FromBody] FinancialOperation operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();

        var foundOperation = await context.FinancialOperations.FirstOrDefaultAsync(x => x.Id == operation.Id && x.IdUserAdd == operation.IdUserAdd);
        
        if (foundOperation == null) return NotFound("Операция не найдена!");
        
        foundOperation.IdCategory = operation.IdCategory;
        foundOperation.IdUserAdd = operation.IdUserAdd;
        foundOperation.Date = operation.Date;
        foundOperation.OperationType = operation.OperationType;
        foundOperation.Value = operation.Value;
        
        context.FinancialOperations.Update(foundOperation);
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFinancialOperation([FromBody] FinancialOperation operation)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        
        var foundOperation = await context.FinancialOperations.FirstOrDefaultAsync(x => x.Id == operation.Id && x.IdUserAdd == operation.IdUserAdd);
        
        if (foundOperation == null) return NotFound("Операция не найдена!");
        
        context.FinancialOperations.Remove(foundOperation);
        _ = await context.SaveChangesAsync();
        
        return NoContent();
    }
}