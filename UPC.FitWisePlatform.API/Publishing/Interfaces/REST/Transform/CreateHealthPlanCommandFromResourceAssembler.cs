using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class CreateHealthPlanCommandFromResourceAssembler
{
    public static CreateHealthPlanCommand ToCommandFromResource(CreateHealthPlanResource resource)
    {
        if (!Enum.TryParse(resource.DurationType, true, out DurationUnit durationUnit))
        {
            throw new ArgumentException($"Invalid DurationUnit value: '{resource.DurationValue}'." +
                                        $" Must be a valid DurationUnitType enum value " +
                                        $"(e.g., 'Day', 'Week', 'Month').");
        }
        if (!Enum.TryParse(resource.Currency, true, out Currency currency))
        {
            throw new ArgumentException($"Invalid Currency value: '{resource.Currency}'. " +
                                        $"Must be a valid CurrencyType enum value (e.g., 'USD', 'EUR', 'PEN').");
        }

        return new CreateHealthPlanCommand(
            resource.PlanName, 
            resource.Objective,
            new Duration(resource.DurationValue, durationUnit),
            new Price(resource.PriceValue, currency),
            resource.Description,
            resource.ProfileId);
    }
}