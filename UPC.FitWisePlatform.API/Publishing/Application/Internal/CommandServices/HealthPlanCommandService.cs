using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class HealthPlanCommandService(IHealthPlanRepository healthPlanRepository,
    IUnitOfWork  unitOfWork) : IHealthPlanCommandService
{
    public async Task<HealthPlan?> Handle(CreateHealthPlanCommand command)
    {
        if (await healthPlanRepository.ExistsByNameAsync(command.Name))
            throw new Exception("HealthPlan with the same name already exists");
        var healthPlan = new HealthPlan(command);
        await healthPlanRepository.AddAsync(healthPlan);
        await unitOfWork.CompleteAsync();
        return healthPlan;
    }
}