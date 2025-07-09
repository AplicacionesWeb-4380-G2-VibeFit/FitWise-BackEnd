using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class CreateMealCommandFromResourceAssembler
{
    public static CreateMealCommand ToCommandFromResource(CreateMealResource resource)
    {
        return new CreateMealCommand(resource.Name, resource.Description, resource.ImageUri);
    }
}