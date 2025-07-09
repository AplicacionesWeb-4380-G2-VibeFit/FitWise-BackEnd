using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;

public class HealthPlanExerciseQueryService(
    IHealthPlanExerciseRepository healthPlanExerciseRepository) : IHealthPlanExerciseQueryService
{
    public async Task<HealthPlanExercise?> Handle(GetHealthPlanExerciseByIdQuery query)
    {
        return await healthPlanExerciseRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<HealthPlanExercise>> Handle(GetHealthPlanExercisesByHealthPlanIdQuery query)
    {
        if (query.DayOfWeek.HasValue &&
            query.DayOfWeek.Value != DayOfWeekType.Unknown &&
            query.DayOfWeek.Value != DayOfWeekType.EveryDay)
        {
            return await healthPlanExerciseRepository.FindByHealthPlanIdAndDayOfWeekAsync(query.HealthPlanId, query.DayOfWeek.Value);
        }
        else
        {
            return await healthPlanExerciseRepository.FindByHealthPlanIdAsync(query.HealthPlanId);
        }
    }
}