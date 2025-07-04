namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record CreateReviewReportResource(int ReviewId, int UserId, string Reason);