using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Repositories;

public interface IHealthPlanExerciseRepository : IBaseRepository<HealthPlanExercise>
{
    Task<IEnumerable<HealthPlanExercise>> FindByHealthPlanIdAsync(int healthPlanId);
    Task<bool> ExistsSameAssignmentOnDayOfWeekAsync(int healthPlanId, int exerciseId, DayOfWeekType dayOfWeek);
}