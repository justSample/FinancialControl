namespace Backend.Models.Db;

public class FinancialOperation
{
    public int Id { get; set; }
    public int IdUserAdd { get; set; }
    public int IdCategory { get; set; }
    
    public FinancialOperationType OperationType { get; set; }
    
    public decimal Value { get; set; }
    
    public DateTime Date { get; set; }
}