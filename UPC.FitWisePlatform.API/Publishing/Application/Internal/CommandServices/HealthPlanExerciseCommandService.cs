using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class HealthPlanExerciseCommandService(
    IHealthPlanRepository healthPlanRepository,
    IExerciseRepository exerciseRepository,
    IHealthPlanExerciseRepository healthPlanExerciseRepository,
    IUnitOfWork unitOfWork) : IHealthPlanExerciseCommandService
{
    public async Task<HealthPlanExercise?> Handle(AssignExerciseToHealthPlanCommand command)
    {
        var healthPlan = await healthPlanRepository.FindByIdAsync(command.HealthPlanId);
        if (healthPlan == null)
        {
            throw new ArgumentException($"Health Plan with ID {command.HealthPlanId} not found.");
        }
        var exercise = await exerciseRepository.FindByIdAsync(command.ExerciseId);
        if (exercise == null)
        {
            throw new ArgumentException($"Exercise with ID {command.ExerciseId} not found.");
        }
        if (await healthPlanExerciseRepository.ExistsSameAssignmentOnDayOfWeekAsync(command.HealthPlanId,
                command.ExerciseId, command.DayOfWeek))
        {
            throw new Exception($"Health Plan Exercise with the same healthPlanId: {command.HealthPlanId}, " +
                                $"exerciseId: {command.ExerciseId}, dayOfWeek: {command.DayOfWeek} already exists.");
        }
        
        var healthPlanExercise = new HealthPlanExercise(command.HealthPlanId, command.ExerciseId, command.DayOfWeek,
            command.Instructions, command.Sets, command.Reps, command.DurationMinutes);
        await healthPlanExerciseRepository.AddAsync(healthPlanExercise);
        await unitOfWork.CompleteAsync();
        return healthPlanExercise;
    }

    public async Task<HealthPlanExercise?> Handle(UpdateHealthPlanExerciseCommand command)
    {
        var healthPlanExercise = await healthPlanExerciseRepository.FindByIdAsync(command.Id);
        
        if (healthPlanExercise == null)
            throw new ArgumentException($"Health Plan Exercise with ID {command.Id} not found.");
        if (healthPlanExercise.HealthPlanId != command.HealthPlanId || healthPlanExercise.ExerciseId != command.ExerciseId)
            throw new ArgumentException($"Health Plan Id: {command.HealthPlanId} or Exercise Id: {command.ExerciseId} not" +
                                        $" correspond to the Health Plan Exercise.");
        if (await healthPlanExerciseRepository.ExistsSameAssignmentOnDayOfWeekAsync(command.HealthPlanId,
                command.ExerciseId, command.DayOfWeek))
        {
            throw new Exception($"Health Plan Exercise with the same healthPlanId: {command.HealthPlanId}, " +
                                $"exerciseId: {command.ExerciseId}, dayOfWeek: {command.DayOfWeek} already exists.");
        }
        
        healthPlanExercise.UpdateDetails(command.DayOfWeek, command.Instructions, command.Sets, command.Reps,
            command.DurationMinutes);
        healthPlanExerciseRepository.Update(healthPlanExercise);
        await unitOfWork.CompleteAsync();
        
        return healthPlanExercise;
    }

    public async Task<bool> Handle(DeleteHealthPlanExerciseCommand command)
    {
        var healthPlanExercise = await healthPlanExerciseRepository.FindByIdAsync(command.Id);
        if (healthPlanExercise == null)
            throw new ArgumentException($"Health Plan Exercise with ID {command.Id} not found.");
        healthPlanExerciseRepository.Remove(healthPlanExercise);
        await unitOfWork.CompleteAsync();
        return true;
    }
}