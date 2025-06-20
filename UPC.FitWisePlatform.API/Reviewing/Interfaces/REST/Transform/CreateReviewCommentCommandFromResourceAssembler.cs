using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

public static class CreateReviewCommentCommandFromResourceAssembler
{
    public static CreateReviewCommentCommand ToCommandFromResource(CreateReviewCommentResource resource)
    {
        return new CreateReviewCommentCommand(resource.ReviewId, resource.UserId, resource.Content);
    }
}