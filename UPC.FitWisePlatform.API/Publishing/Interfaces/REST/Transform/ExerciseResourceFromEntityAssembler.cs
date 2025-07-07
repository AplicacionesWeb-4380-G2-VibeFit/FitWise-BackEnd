using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class ExerciseResourceFromEntityAssembler
{
    public static ExerciseResource ToResourceFromEntity(Exercise entity)
    {
        return new ExerciseResource(entity.Id, entity.Name, entity.Description, entity.ImageUri);
    }
}