using Backend.Models.Db;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : Controller
{

    [HttpGet]
    public IActionResult GetCategories()
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult AddCategory([FromBody] Category category)
    {
        return Created();
    }
    
    [HttpPut("{id}")]
    public IActionResult EditCategory(int id, [FromBody] Category category)
    {
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        return NoContent();
    }
    
}