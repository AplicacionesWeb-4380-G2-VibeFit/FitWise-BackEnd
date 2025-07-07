using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class MealResourceFromEntityAssembler
{
    public static MealResource ToResourceFromEntity(Meal entity)
    {
        return new MealResource(entity.Id, entity.Name, entity.Description, entity.ImageUri);
    }
}