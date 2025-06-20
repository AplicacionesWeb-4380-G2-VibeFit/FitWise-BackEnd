using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanCommandService
{
    Task<HealthPlan?> Handle(CreateHealthPlanCommand command);
    
    Task<HealthPlan?> Handle(UpdateHealthPlanCommand command);
    
    Task<bool> Handle(DeleteHealthPlanCommand command);
}