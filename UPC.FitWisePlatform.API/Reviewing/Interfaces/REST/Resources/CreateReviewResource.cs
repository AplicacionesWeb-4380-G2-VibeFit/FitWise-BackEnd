namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record CreateReviewResource(string UserId, int Score, string Description, int HealthPlanId);