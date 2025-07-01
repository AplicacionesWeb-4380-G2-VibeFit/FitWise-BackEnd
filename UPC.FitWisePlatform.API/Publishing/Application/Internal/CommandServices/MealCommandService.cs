using Nuevo_FitWise_Ordenado.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.CommandServices;

public class MealCommandService(
    IMealRepository mealRepository,
    IUnitOfWork unitOfWork) : IMealCommandService
{
    public async Task<Meal?> Handle(CreateMealCommand command)
    {
        if (await mealRepository.ExistsByNameAsync(command.Name))
            throw new Exception("Meal with the same name already exists");
        
        var meal = new Meal(
            command.Name,
            command.Description,
            command.ImageUri);
        
        await mealRepository.AddAsync(meal);
        await unitOfWork.CompleteAsync();
        
        return meal;
    }

    public async Task<Meal?> Handle(UpdateMealCommand command)
    {
        var meal = await mealRepository.FindByIdAsync(command.Id);
        
        if (meal == null)
            throw new Exception($"Meal with id '{command.Id}' does not exist");
        if (command.Name != meal.Name && await mealRepository.ExistsByNameAsync(command.Name))
            throw new Exception("Meal with the same name already exists");
        
        meal.UpdateDetails(command.Name, command.Description, command.ImageUri);
        mealRepository.Update(meal);
        await unitOfWork.CompleteAsync();
        
        return meal;
    }
}