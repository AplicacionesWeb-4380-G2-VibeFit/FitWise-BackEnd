namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record AssignMealToHealthPlanResource(int HealthPlanId, int MealId, string DayOfWeek, string MealTime,
    string Notes);