namespace Backend.Models.Db;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public int? IdPartner { get; set; }
    
    public decimal? StartBalance { get; set; }
}