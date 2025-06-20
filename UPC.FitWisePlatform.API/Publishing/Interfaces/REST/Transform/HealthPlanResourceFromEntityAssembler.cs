using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class HealthPlanResourceFromEntityAssembler
{
    public static HealthPlanResource ToResourceFromEntity(HealthPlan entity)
    {
        return new HealthPlanResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Price.Amount,
            entity.Price.Currency,
            entity.Duration.Value,
            entity.Duration.Unit,
            entity.Description,
            entity.CreatorId);
    }
}