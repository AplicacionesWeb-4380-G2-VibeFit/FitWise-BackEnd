using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public record CreatePaymentCommand(
    string OwnerId,
    string PlanId,
    decimal Amount,
    string Currency,
    string Method,
    string Status,
    DateTime PaymentDate,
    int? PurchasedPlanId
);