using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Api;

public class FinancialModel
{
    [Required] public int IdUser { get; set; }
    [Required] public int CategoryId { get; set; }
    [Required] public int OperationTypeInt { get; set; }
    [Required] public decimal Value { get; set; }
}