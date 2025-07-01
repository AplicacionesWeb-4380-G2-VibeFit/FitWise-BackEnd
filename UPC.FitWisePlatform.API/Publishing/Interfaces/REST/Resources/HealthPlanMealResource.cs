namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record HealthPlanMealResource(int Id, int HealthPlanId, int MealId, string DayOfWeek, string MealTime,
    string Notes);