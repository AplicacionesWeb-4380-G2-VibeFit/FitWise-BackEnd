namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record ReviewReportResource(int Id, int ReviewId, int UserId, string Reason, string Status, DateTime CreatedAt);