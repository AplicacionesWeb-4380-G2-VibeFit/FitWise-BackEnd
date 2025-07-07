using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

public class PurchasedPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ✅ Auto-incremento desde EF Core
    public int Id { get; set; }

    public string OwnerId { get; set; } = string.Empty;

    public string PlanId { get; set; } = string.Empty;

    public DateTime PurchaseDate { get; set; }

    public string Status { get; set; } = "active";
}