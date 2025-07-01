using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanMealCommandService
{
    Task<HealthPlanMeal?> Handle(AssignMealToHealthPlanCommand command);
    Task<HealthPlanMeal?> Handle(UpdateHealthPlanMealCommand command);
    Task<bool> Handle(DeleteHealthPlanMealCommand command);
}