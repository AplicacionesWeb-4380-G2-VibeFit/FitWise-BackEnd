namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record ReviewResource(int Id, string UserId, int Score, string Description, int HealthPlanId);