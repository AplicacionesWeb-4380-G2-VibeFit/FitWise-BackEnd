using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string OwnerId { get; set; } = string.Empty;

    public string PlanId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Method { get; set; } = string.Empty;

    public string Status { get; set; } = "pending";

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public int? PurchasedPlanId { get; set; }
}