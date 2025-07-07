namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public class UpdatePurchasedPlanCommand
{
    public string OwnerId { get; set; } = string.Empty;
    public string PlanId { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string Status { get; set; } = "active";
}
