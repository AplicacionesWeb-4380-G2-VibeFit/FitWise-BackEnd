using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class HealthPlanMealCommandService(
    IHealthPlanRepository healthPlanRepository,
    IMealRepository mealRepository,
    IHealthPlanMealRepository healthPlanMealRepository,
    IUnitOfWork unitOfWork) : IHealthPlanMealCommandService
{
    public async Task<HealthPlanMeal?> Handle(AssignMealToHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.HealthPlanId);
        if (healthPlan == null)
        {
            throw new ArgumentException($"Health Plan with ID {command.HealthPlanId} not found.");
        }
        var meal  = await mealRepository.FindByIdAsync(command.MealId);
        if (meal == null)
        {
            throw new ArgumentException($"Meal with ID {command.MealId} not found.");
        }
        if (await healthPlanMealRepository.ExistsSameAssignmentOnDayOfWeekAsync(command.HealthPlanId,
                command.MealId, command.DayOfWeek))
        {
            throw new Exception($"Health Plan Exercise with the same healthPlanId: {command.HealthPlanId}, " +
                                $"mealId: {command.MealId}, dayOfWeek: {command.DayOfWeek}" + " already exists.");
        }

        var healthPlanMeal = new HealthPlanMeal(command.HealthPlanId, command.MealId, command.DayOfWeek,
            command.MealTime, command.Notes);
        await healthPlanMealRepository.AddAsync(healthPlanMeal);
        await unitOfWork.CompleteAsync();
        return healthPlanMeal;
    }

    public async Task<HealthPlanMeal?> Handle(UpdateHealthPlanMealCommand command)
    {
        var healthPlanMeal = await healthPlanMealRepository.FindByIdAsync(command.Id);

        if (healthPlanMeal == null)
            throw new ArgumentException($"Health Plan with ID {command.HealthPlanId} not found.");
        if (healthPlanMeal.HealthPlanId != command.HealthPlanId || healthPlanMeal.MealId != command.MealId)
            throw new ArgumentException($"Health Plan Id: {command.HealthPlanId} or Meal Id: {command.MealId} not " +
                                        $"correspond to the Health Plan Meal.");
        if (await healthPlanMealRepository.ExistsSameAssignmentOnDayOfWeekAsync(command.HealthPlanId,
                command.MealId, command.DayOfWeek))
        {
            throw new Exception($"Health Plan Meal with the same healthPlanId: {command.HealthPlanId}, " +
                                $"mealId: {command.MealId}, dayOfWeek: {command.DayOfWeek}, " + " already exists.");
        }
        
        healthPlanMeal.UpdateDetails(command.DayOfWeek, command.MealTime, command.Notes);
        healthPlanMealRepository.Update(healthPlanMeal);
        await unitOfWork.CompleteAsync();
        return healthPlanMeal;
    }

    public async Task<bool> Handle(DeleteHealthPlanMealCommand command)
    {
        var healthPlanMeal = await healthPlanMealRepository.FindByIdAsync(command.Id);
        if (healthPlanMeal == null)
            throw new ArgumentException($"Health Plan Meal with ID {command.Id} not found.");
        healthPlanMealRepository.Remove(healthPlanMeal);
        await unitOfWork.CompleteAsync();
        return true;
    }
}