using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

public record GetHealthPlanMealsByHealthPlanIdQuery(int HealthPlanId, DayOfWeekType? DayOfWeek = null);