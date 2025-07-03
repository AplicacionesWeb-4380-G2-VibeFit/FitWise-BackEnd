namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record ReviewReportResource(int Id, int ReviewId, string UserId, string Reason, string Status, DateTime CreatedAt);