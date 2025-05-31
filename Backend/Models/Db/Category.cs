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
    
    [Column("idParent")]
    public int? IdParent { get; set; }
    
    public User User { get; set; }
    public Category? ParentCategory { get; set; }

    public List<Category> ChildCategories { get; set; } = new();
}