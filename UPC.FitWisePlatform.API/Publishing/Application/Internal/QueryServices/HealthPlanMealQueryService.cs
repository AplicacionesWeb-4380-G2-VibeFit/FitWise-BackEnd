using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;

public class HealthPlanMealQueryService(
    IHealthPlanMealRepository healthPlanMealRepository) : IHealthPlanMealQueryService

{
    public async Task<HealthPlanMeal?> Handle(GetHealthPlanMealByIdQuery query)
    {
        return await healthPlanMealRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<HealthPlanMeal>> Handle(GetHealthPlanMealsByHealthPlanIdQuery query)
    {
        return await healthPlanMealRepository.FindByHealthPlanIdAsync(query.HealthPlanId);
    }
}