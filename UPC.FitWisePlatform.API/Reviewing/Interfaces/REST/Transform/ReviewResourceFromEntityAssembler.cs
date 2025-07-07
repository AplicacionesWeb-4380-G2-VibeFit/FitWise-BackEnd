using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Reviewing.Interfaces.REST.Transform;

public static class ReviewResourceFromEntityAssembler
{
    public static ReviewResource ToResourceFromEntity(Review entity)
    {
        return new ReviewResource(entity.Id, entity.UserId, entity.Score, entity.Description, entity.HealthPlanId);
    }
}