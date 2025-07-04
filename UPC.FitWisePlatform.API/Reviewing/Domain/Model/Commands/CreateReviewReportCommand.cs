namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

public record CreateReviewReportCommand(int ReviewId, int UserId, string Reason);