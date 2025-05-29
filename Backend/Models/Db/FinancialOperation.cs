using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Db;

[Table("financial_operations")]
public class FinancialOperation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    [Column("idUserAdd")]
    public Guid IdUserAdd { get; set; }
    
    // TODO: Категория может быть удалена. Придумай что-нибудь
    [ForeignKey(nameof(Category))]
    [Column("idCategory")]
    public int IdCategory { get; set; }
    
    [Column("operationType")]
    public FinancialOperationType OperationType { get; set; }
    
    [Column("value")]
    public decimal Value { get; set; }
    
    [Column("date")]
    public DateTime Date { get; set; }
    
    public User User { get; set; }
    public Category Category { get; set; }
}