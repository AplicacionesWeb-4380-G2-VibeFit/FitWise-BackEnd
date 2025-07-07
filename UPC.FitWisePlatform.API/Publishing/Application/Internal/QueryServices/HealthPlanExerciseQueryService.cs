using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
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
        return await healthPlanExerciseRepository.FindByHealthPlanIdAsync(query.HealthPlanId);
    }
}