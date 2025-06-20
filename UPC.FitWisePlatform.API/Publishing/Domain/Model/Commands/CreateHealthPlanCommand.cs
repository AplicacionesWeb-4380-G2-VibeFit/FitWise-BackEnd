namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record CreateHealthPlanCommand(
    string Name, 
    string Objective, 
    decimal PriceAmount, 
    string PriceCurrency,
    int DurationValue,
    string DurationUnit,
    string Description,
    int CreatorId);