namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record CreateReviewReportResource(int ReviewId, string UserId, string Reason);