namespace Backend.Models.Api;

public class CategoryModel
{
    public int IdUser { get; set; }
    public int? IdParent { get; set; }
    
    public string Name { get; set; }
    public int OperationType { get; set; }
}