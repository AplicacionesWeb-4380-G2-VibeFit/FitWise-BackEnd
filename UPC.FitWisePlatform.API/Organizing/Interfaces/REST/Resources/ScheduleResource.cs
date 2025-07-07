namespace UPC.FitWisePlatform.API.Organizing.Interfaces.REST.Resources;

public record ScheduleResource(int Id, int UserId, int HealthPlanId, DateTime Date);