namespace Backend.Models.Db;

public class Category
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public string Name { get; set; }
    
    public FinancialOperationType OperationType { get; set; }
    
    public int? IdParent { get; set; }
}