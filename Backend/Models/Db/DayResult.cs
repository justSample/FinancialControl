namespace Backend.Models.Db;

public class DayResult
{
    public int Id { get; set; }
    public Guid IdUser { get; set; }
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
}