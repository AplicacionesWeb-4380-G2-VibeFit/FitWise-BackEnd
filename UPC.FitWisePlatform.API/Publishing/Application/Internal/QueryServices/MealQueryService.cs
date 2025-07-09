using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;

public class MealQueryService(
    IMealRepository mealRepository) : IMealQueryService
{
    public async Task<Meal?> Handle(GetMealByIdQuery query)
    {
        return await mealRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Meal>> Handle(GetAllMealsQuery query)
    {
        return await mealRepository.ListAsync();
    }
}