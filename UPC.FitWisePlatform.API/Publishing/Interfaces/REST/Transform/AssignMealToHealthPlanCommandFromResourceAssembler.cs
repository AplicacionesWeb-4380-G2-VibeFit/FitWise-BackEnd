using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Transform;

public static class AssignMealToHealthPlanCommandFromResourceAssembler
{
    public static AssignMealToHealthPlanCommand ToCommandFromResource(AssignMealToHealthPlanResource resource)
    {
        if (!Enum.TryParse(resource.DayOfWeek, true, out DayOfWeekType dayOfWeekType))
        {
            throw new ArgumentException($"Invalid DayOfWeek value: {resource.DayOfWeek}");
        }
        if (!Enum.TryParse(resource.MealTime, true, out MealTime mealTimeType))
        {
            throw new ArgumentException($"Invalid MealTime value: {resource.MealTime}");
        }

        return new AssignMealToHealthPlanCommand(resource.HealthPlanId, resource.MealId, dayOfWeekType, mealTimeType,
            resource.Notes);
    }
}