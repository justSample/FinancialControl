using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Db;

[Table("categories")]
public class Category
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey(nameof(User))]
    [Column("idUser")]
    public Guid IdUser { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("operationType", TypeName = "int")]
    public FinancialOperationType OperationType { get; set; }
    
    [Column("idParent")]
    public int? IdParent { get; set; }
    
    public User User { get; set; }
}