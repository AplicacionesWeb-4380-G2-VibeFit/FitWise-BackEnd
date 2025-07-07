using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class CreateExerciseCommandFromResourceAssembler
{
    public static CreateExerciseCommand ToCommandFromResource(CreateExerciseResource resource)
    {
        return new CreateExerciseCommand(resource.Name, resource.Description, resource.ImageUri);
    }
}