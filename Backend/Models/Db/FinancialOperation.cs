using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Db;

[Table("financial_operations")]
public class FinancialOperation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Day))]
    public int IdDay { get; set; } 
    
    [ForeignKey(nameof(User))]
    [Column("idUser")]
    public Guid IdUser { get; set; }
    
    [ForeignKey(nameof(Category))]
    [Column("idCategory")]
    public int? IdCategory { get; set; }
    
    [Column("operationType")]
    public FinancialOperationType OperationType { get; set; }
    
    [Column("value")]
    public decimal Value { get; set; }
    
    public User User { get; set; }
    public Day Day { get; set; }
    public Category? Category { get; set; }
}