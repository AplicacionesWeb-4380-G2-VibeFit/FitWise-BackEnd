using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class HealthPlanMealResourceFromEntityAssembler
{
    public static HealthPlanMealResource ToResourceFromEntity(HealthPlanMeal entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity),
                "Cannot convert a null HealthPlan entity to a HealthPlanResource.");
        }

        return new HealthPlanMealResource(entity.Id, entity.HealthPlanId, entity.MealId, entity.DayOfWeek.ToString(),
            entity.MealTime.ToString(), entity.Notes);
    }
}