using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
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
        if (query.DayOfWeek.HasValue &&
            query.DayOfWeek.Value != DayOfWeekType.Unknown &&
            query.DayOfWeek.Value != DayOfWeekType.EveryDay)
        {
            // Usamos el nuevo método del repositorio para filtrar por día
            return await healthPlanMealRepository.FindByHealthPlanIdAndDayOfWeekAsync(query.HealthPlanId, query.DayOfWeek.Value);
        }
        else
        {
            // Si DayOfWeek no se proporciona, o es Unknown/EveryDay,
            // devolvemos todos los meals para ese plan de salud
            return await healthPlanMealRepository.FindByHealthPlanIdAsync(query.HealthPlanId);
        }
    }
}