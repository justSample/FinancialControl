namespace Backend.Models.Api;

public class CategoryModel
{
    public Guid IdUser { get; set; }
    public int? IdParent { get; set; } = null;
    
    public string Name { get; set; }
    public int OperationType { get; set; }
}