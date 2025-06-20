using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class CreateHealthPlanCommandFromResourceAssembler
{
    public static CreateHealthPlanCommand ToCommandFromResource(CreateHealthPlanResource resource)
    {
        return new CreateHealthPlanCommand(
            resource.Name, 
            resource.Objective, 
            resource.PriceAmount,
            resource.PriceCurrency,
            resource.DurationValue,
            resource.DurationUnit,
            resource.Description,
            resource.CreatorId);
    }
}