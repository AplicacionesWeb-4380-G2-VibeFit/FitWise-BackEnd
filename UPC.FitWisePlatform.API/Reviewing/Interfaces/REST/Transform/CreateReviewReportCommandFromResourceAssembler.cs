using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

public static class CreateReviewReportCommandFromResourceAssembler
{
    public static CreateReviewReportCommand ToCommandFromResource(CreateReviewReportResource resource)
    {
        return new CreateReviewReportCommand(resource.ReviewId, resource.UserId, resource.Reason);
    }
}