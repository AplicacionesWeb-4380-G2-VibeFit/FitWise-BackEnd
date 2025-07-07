using Nuevo_FitWise_Ordenado.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IMealCommandService
{
    Task<Meal?> Handle(CreateMealCommand command);
    Task<Meal?> Handle(UpdateMealCommand command);
}