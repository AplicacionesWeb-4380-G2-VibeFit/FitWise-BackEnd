namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

public record CreateReviewCommentResource(int ReviewId, string UserId, string Content);