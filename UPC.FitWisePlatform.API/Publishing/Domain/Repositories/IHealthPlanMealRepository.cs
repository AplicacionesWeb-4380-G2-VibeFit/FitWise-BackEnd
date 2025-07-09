using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Repositories;

public interface IHealthPlanMealRepository : IBaseRepository<HealthPlanMeal>
{
    Task<IEnumerable<HealthPlanMeal>> FindByHealthPlanIdAsync(int healthPlanId);
    Task<IEnumerable<HealthPlanMeal>> FindByHealthPlanIdAndDayOfWeekAsync(int healthPlanId, DayOfWeekType dayOfWeek);
    Task<bool> ExistsSameAssignmentOnDayOfWeekAsync(int healthPlanId, int mealId, DayOfWeekType dayOfWeek);
}