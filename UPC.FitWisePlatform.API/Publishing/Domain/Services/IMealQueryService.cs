using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IMealQueryService
{
    Task<Meal?> Handle(GetMealByIdQuery query);
    Task<IEnumerable<Meal>> Handle(GetAllMealsQuery query);
}