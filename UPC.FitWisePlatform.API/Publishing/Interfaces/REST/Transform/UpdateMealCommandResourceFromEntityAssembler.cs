using Nuevo_FitWise_Ordenado.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class UpdateMealCommandResourceFromEntityAssembler
{
    public static UpdateMealCommand ToCommandFromResource(int id, UpdateMealResource resource)
    {
        return new UpdateMealCommand(id, resource.Name, resource.Description, resource.ImageUri);
    }
}