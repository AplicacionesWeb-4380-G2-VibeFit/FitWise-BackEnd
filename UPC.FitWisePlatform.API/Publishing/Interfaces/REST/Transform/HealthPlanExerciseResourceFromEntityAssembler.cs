using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class HealthPlanExerciseResourceFromEntityAssembler
{
    public static HealthPlanExerciseResource ToResourceFromEntity(HealthPlanExercise entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Cannot convert a null HealthPlan entity to a HealthPlanResource.");
        }
        
        return new HealthPlanExerciseResource(entity.Id, entity.HealthPlanId, entity.ExerciseId,
            entity.DayOfWeek.ToString(), entity.Instructions, entity.Sets, entity.Reps, entity.DurationInMinutes);
    }
}