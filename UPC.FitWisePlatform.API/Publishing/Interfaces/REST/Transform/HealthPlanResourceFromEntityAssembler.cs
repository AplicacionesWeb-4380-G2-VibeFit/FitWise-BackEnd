using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class HealthPlanResourceFromEntityAssembler
{
    public static HealthPlanResource ToResourceFromEntity(HealthPlan entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Cannot convert a null HealthPlan entity to a HealthPlanResource.");
        }

        return new HealthPlanResource(
            entity.Id,
            entity.PlanName,
            entity.Objective,
            entity.Duration.DurationValue,
            entity.Duration.DurationType.ToString(),
            entity.Price.PriceValue,
            entity.Price.Currency.ToString(),
            entity.Description,
            entity.ProfileId);
    }
}