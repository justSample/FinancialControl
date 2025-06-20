using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Db;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("username")]
    public string Username { get; set; }
    
    [Column("login")]
    public string Login { get; set; }
    [Column("passwordHash")]
    public string PasswordHash { get; set; }
    
    [Column("idPartner")]
    public Guid? IdPartner { get; set; }
    
    [Column("startBalance")]
    public decimal StartBalance { get; set; }
    
    [ForeignKey(nameof(Role))]
    [Column("idRole")]
    public int? IdRole { get; set; }
    
    public User? Partner { get; set; }
    public Role? Role { get; set; }
    public List<User> Partners { get; set; } = new();
    
    public List<Category> Categories { get; set; } = new();
    public List<FinancialOperation> FinancialOperations { get; set; } = new();
    public List<Day> Days { get; set; } = new();
}