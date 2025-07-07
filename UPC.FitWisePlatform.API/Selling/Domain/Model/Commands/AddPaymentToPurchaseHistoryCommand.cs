using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public record AddPaymentToPurchaseHistoryCommand(
    string UserId,
    Payment NewPayment
);