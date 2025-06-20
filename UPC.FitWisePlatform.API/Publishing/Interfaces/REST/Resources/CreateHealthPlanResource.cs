namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record CreateHealthPlanResource(
    string Name, 
    string Objective, 
    decimal PriceAmount, 
    string PriceCurrency,
    int DurationValue,
    string DurationUnit,
    string Description,
    int CreatorId);