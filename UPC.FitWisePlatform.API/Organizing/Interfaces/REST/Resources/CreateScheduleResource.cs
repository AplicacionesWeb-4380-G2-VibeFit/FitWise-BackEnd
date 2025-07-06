namespace UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;

public record CreateScheduleResource(int UserId, int HealthPlanId, DateTime Date);