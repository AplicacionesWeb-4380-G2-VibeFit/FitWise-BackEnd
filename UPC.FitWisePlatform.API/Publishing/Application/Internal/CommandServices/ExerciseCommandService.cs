using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class ExerciseCommandService(
    IExerciseRepository exerciseRepository,
    IUnitOfWork unitOfWork) : IExerciseCommandService
{
    public async Task<Exercise?> Handle(CreateExerciseCommand command)
    {
        if (await exerciseRepository.ExistsByNameAsync(command.Name))
            throw new Exception("Exercise with the same name already exists");
        
        var exercise = new Exercise(command.Name, command.Description, command.ImageUri);
        
        await exerciseRepository.AddAsync(exercise);
        await unitOfWork.CompleteAsync();
        
        return exercise;
    }

    public async Task<Exercise?> Handle(UpdateExerciseCommand command)
    {
        var exercise = await exerciseRepository.FindByIdAsync(command.Id);
        
        if (exercise == null)
            throw new Exception($"Meal with id '{command.Id}' does not exist");
        if (command.Name != exercise.Name && await exerciseRepository.ExistsByNameAsync(command.Name))
            throw new Exception("Meal with the same name already exists");
        
        exercise.UpdateDetails(command.Name, command.Description, command.ImageUri);
        exerciseRepository.Update(exercise);
        await unitOfWork.CompleteAsync();
        
        return exercise;
    }
}