using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class UpdateExerciseCommandFromResourceAssembler
{
    public static UpdateExerciseCommand ToCommandFromResource(int id, UpdateExerciseResource resource)
    {
        return new UpdateExerciseCommand(id, resource.Name, resource.Description, resource.ImageUri);
    }
}