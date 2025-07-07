using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanQueryService
{
    Task<HealthPlan?> Handle(GetHealthPlanByIdQuery query);
    Task<IEnumerable<HealthPlan>> Handle(GetAllHealthPlansQuery query);
    Task<IEnumerable<HealthPlan>> Handle(GetHealthPlansByProfileIdQuery query);
}