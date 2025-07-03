namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

public record CreateReviewReportCommand(int ReviewId, string UserId, string Reason);