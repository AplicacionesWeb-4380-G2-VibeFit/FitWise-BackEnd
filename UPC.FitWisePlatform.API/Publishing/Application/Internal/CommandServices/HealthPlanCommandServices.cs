using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class HealthPlanCommandService(
    IHealthPlanRepository healthPlanRepository,
    IUnitOfWork unitOfWork) : IHealthPlanCommandService
{
    public async Task<HealthPlan?> Handle(CreateHealthPlanCommand command)
    {
        if (await healthPlanRepository.ExistsByPlanNameAsync(command.PlanName))
            throw new Exception("Health Plan with the same plan name already exists");
        
        var healthPlan = new HealthPlan(
            command.PlanName, 
            command.Objective,
            command.Duration, 
            command.Price, 
            command.Description, 
            command.ProfileId);
        
        await healthPlanRepository.AddAsync(healthPlan);
        await unitOfWork.CompleteAsync();
        
        return healthPlan;
    }

    public async Task<HealthPlan?> Handle(UpdateHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.Id);
        
        if (healthPlan == null)
            throw new Exception($"HealthPlan with id '{command.Id}' does not exist");
        if(command.PlanName != healthPlan.PlanName && await healthPlanRepository.ExistsByPlanNameAsync(command.PlanName))
            throw new Exception("Health Plan with the same plan name already exists");
        
        healthPlan.UpdateDetails(command.PlanName, command.Objective, command.Duration, command.Price, command.Description);
        healthPlanRepository.Update(healthPlan);
        await unitOfWork.CompleteAsync();
        
        return healthPlan;
    }

    public async Task<bool> Handle(DeleteHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.Id);
        if (healthPlan == null)
            throw new Exception($"HealthPlan with id '{command.Id}' does not exist");
        healthPlanRepository.Remove(healthPlan);
        await unitOfWork.CompleteAsync();
        return true;
    }
}