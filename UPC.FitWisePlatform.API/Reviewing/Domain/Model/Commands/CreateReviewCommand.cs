namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

public record CreateReviewCommand(string UserId, int Score, string Description, int HealthPlanId);