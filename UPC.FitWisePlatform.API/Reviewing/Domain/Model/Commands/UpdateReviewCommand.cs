namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

public record UpdateReviewCommand(int Id, int Score, string Description);