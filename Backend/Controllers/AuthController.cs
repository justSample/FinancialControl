using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Models.Api;
using Backend.Models.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;

namespace Backend.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserLoginModel userDto)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        
        if (await context.Users.AnyAsync(u => u.Login == userDto.Login))
            return BadRequest("Пользователь уже создан");

        if (string.IsNullOrWhiteSpace(userDto.Username))
            return BadRequest("Имя пользователя не должно быть пустым");
        
        User user = new User
        {
            Username = userDto.Username,
            Login = userDto.Login,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel userDto)
    {
        await using FinancialDbContext context = new FinancialDbContext();
        
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == userDto.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            return Unauthorized();

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(2d),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}