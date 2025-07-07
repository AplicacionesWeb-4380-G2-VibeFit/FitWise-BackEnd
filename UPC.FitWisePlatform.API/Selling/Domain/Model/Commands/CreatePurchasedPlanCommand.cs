using System;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public class CreatePurchasedPlanCommand
{
    public string OwnerId { get; set; } = default!;
    public string PlanId { get; set; } = default!;
    public DateTime PurchaseDate { get; set; }
    public string Status { get; set; } = default!;
}
