using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;

public class HealthPlanQueryService(IHealthPlanRepository healthPlanRepository) : IHealthPlanQueryService
{
    public async Task<IEnumerable<HealthPlan>> Handle(GetAllHealthPlansQuery query)
    {
        return await healthPlanRepository.ListAsync();
    }

    public async Task<HealthPlan?> Handle(GetHealthPlanByIdQuery query)
    {
        return await healthPlanRepository.FindByIdAsync(query.HealthPlanId);
    }

    public async Task<IEnumerable<HealthPlan>> Handle(GetHealthPlansByCreatorIdQuery query)
    {
        return await healthPlanRepository.FindByCreatorIdAsync(query.CreatorId);
    }
}