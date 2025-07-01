using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanMealQueryService
{
    Task<HealthPlanMeal?> Handle(GetHealthPlanMealByIdQuery query);
    Task<IEnumerable<HealthPlanMeal>> Handle(GetHealthPlanMealsByHealthPlanIdQuery query);
}