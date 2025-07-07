namespace UPC.FitWisePlatform.API.Organizing.Domain.Model.Commands;

public record CreateScheduleCommand(int UserId, int HealthPlanId, DateTime Date);