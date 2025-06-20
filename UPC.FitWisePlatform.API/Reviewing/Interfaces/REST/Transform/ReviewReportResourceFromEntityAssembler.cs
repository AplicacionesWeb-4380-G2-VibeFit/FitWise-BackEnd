using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

public static class ReviewReportResourceFromEntityAssembler
{
    public static ReviewReportResource ToResourceFromEntity(ReviewReport entity)
    {
        return new ReviewReportResource(entity.Id, entity.ReviewId, entity.UserId, entity.Reason, entity.Status, entity.CreatedAt);
    }
}