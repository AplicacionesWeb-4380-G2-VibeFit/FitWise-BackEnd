using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class HealthPlanExerciseRepository(AppDbContext context) :
    BaseRepository<HealthPlanExercise>(context), IHealthPlanExerciseRepository
{
    public async Task<IEnumerable<HealthPlanExercise>> FindByHealthPlanIdAsync(int healthPlanId)
    {
        return await Context.Set<HealthPlanExercise>().Where(hpe => hpe.HealthPlanId == healthPlanId).ToListAsync();
    }

    public async Task<IEnumerable<HealthPlanExercise>> FindByHealthPlanIdAndDayOfWeekAsync(int healthPlanId, DayOfWeekType dayOfWeek)
    {
        return await Context.Set<HealthPlanExercise>()
            .Where(hpm => hpm.HealthPlanId == healthPlanId && hpm.DayOfWeek == dayOfWeek)
            .ToListAsync();
    }

    public async Task<bool> ExistsSameAssignmentOnDayOfWeekAsync(int healthPlanId, int exerciseId, DayOfWeekType dayOfWeek)
    {
        return await Context.Set<HealthPlanExercise>().AnyAsync(
            hpe => hpe.HealthPlanId == healthPlanId && hpe.ExerciseId == exerciseId && hpe.DayOfWeek == dayOfWeek);
    }
}