namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record CreateHealthPlanResource(string PlanName, string Objective, int DurationValue, string DurationType, 
    decimal PriceValue, string Currency, string Description, int ProfileId);