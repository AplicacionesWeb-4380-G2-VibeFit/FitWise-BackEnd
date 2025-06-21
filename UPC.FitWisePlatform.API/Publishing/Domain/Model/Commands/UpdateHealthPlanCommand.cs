namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record UpdateHealthPlanCommand(
    int HealthPlanId,
    string Name, 
    string Objective, 
    decimal PriceAmount, 
    string PriceCurrency,
    int DurationValue,
    string DurationUnit,
    string Description);