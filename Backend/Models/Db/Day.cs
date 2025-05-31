using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Db;

[Table("days")]
public class Day
{
    [Key] 
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))] 
    public Guid IdUser { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal Value { get; set; }
    
    public User User { get; set; }

    public List<FinancialOperation> FinancialOperations { get; set; } = new();
}