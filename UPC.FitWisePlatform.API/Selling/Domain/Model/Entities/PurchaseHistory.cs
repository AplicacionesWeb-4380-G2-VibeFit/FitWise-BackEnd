using System.ComponentModel.DataAnnotations;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

public class PurchaseHistory
{
    [Key]
    public string Id { get; set; } = string.Empty; // User ID

    public List<Payment> Payments { get; set; } = new();
}