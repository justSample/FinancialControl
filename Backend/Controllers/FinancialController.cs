using Backend.Models.Api;
using Backend.Models.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("financial")]
public class FinancialController : ControllerBase
{
    [HttpPost("add")]
    public IActionResult AddFinancialOperation([FromBody] FinancialModel operation)
    {
        return Created();
    }

    [HttpPut("update/{id}")]
    public IActionResult EditFinancialOperation(int id, [FromBody] FinancialModel operation)
    {
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteFinancialOperation(int id)
    {
        return NoContent();
    }

    [HttpGet("get")]
    public IActionResult GetFinancialOperations([FromBody] FinancialSearchModel search)
    {
        return Ok();
    }
    
}