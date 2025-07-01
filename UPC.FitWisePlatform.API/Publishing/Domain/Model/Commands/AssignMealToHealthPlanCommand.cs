using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record AssignMealToHealthPlanCommand(int HealthPlanId, int MealId, DayOfWeekType DayOfWeek, MealTime MealTime,
    string Notes);