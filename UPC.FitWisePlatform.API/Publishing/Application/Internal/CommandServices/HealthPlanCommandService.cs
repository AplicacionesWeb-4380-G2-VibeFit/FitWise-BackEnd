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

    public async Task<HealthPlan?> Handle(UpdateHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.HealthPlanId);
        if (healthPlan == null)
            throw new Exception($"HealthPlan with id '{command.HealthPlanId}' does not exist");
        if (command.Name != healthPlan.Name && await healthPlanRepository.ExistsByNameAsync(command.Name))
            throw new Exception($"HealthPlan with the name -> '{command.Name}' already exists");
        healthPlan.UpdateHealthPlan(command);
        healthPlanRepository.Update(healthPlan);
        await unitOfWork.CompleteAsync();
        return healthPlan;
    }

    public async Task<bool> Handle(DeleteHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.HealthPlanId);
        if (healthPlan == null)
            throw new Exception($"HealthPlan with id '{command.HealthPlanId}' does not exist");
        healthPlanRepository.Remove(healthPlan);
        await unitOfWork.CompleteAsync();
        return true;
    }
}