using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

public static class ReviewCommentResourceFromEntityAssembler
{
    public static ReviewCommentResource ToResourceFromEntity(ReviewComment entity)
    {
        return new ReviewCommentResource(entity.Id, entity.ReviewId, entity.UserId, entity.Content, entity.CreatedAt);
    }
}