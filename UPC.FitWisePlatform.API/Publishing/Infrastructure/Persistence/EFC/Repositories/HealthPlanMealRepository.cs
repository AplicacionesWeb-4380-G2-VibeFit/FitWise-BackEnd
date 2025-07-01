using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class HealthPlanMealRepository(AppDbContext context) :
    BaseRepository<HealthPlanMeal>(context), IHealthPlanMealRepository
{
    public async Task<IEnumerable<HealthPlanMeal>> FindByHealthPlanIdAsync(int healthPlanId)
    {
        return await Context.Set<HealthPlanMeal>().Where(hpm => hpm.HealthPlanId == healthPlanId).ToListAsync();
    }

    public async Task<bool> ExistsSameAssignmentOnDayOfWeekAsync(int healthPlanId, int mealId, DayOfWeekType dayOfWeek)
    {
        return await Context.Set<HealthPlanMeal>().AnyAsync(hpm =>
            hpm.HealthPlanId == healthPlanId && hpm.MealId == mealId && hpm.DayOfWeek == dayOfWeek);
    }
}