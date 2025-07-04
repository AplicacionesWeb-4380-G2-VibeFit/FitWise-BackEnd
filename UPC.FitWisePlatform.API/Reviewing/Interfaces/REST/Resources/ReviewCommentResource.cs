namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record ReviewCommentResource(int Id, int ReviewId, int UserId, string Content, DateTime CreatedAt);