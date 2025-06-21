using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class UpdateHealthPlanCommandFromResourceAssembler
{
    public static UpdateHealthPlanCommand ToCommandFromResource(int healthPlanId, UpdateHealthPlanResource resource)
    {
        return new UpdateHealthPlanCommand(
            healthPlanId,
            resource.Name,
            resource.Objective,
            resource.PriceAmount,
            resource.PriceCurrency,
            resource.DurationValue,
            resource.DurationUnit,
            resource.Description);
    }
}