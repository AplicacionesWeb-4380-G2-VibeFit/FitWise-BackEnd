namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record CreateReviewCommentResource(int ReviewId, int UserId, string Content);