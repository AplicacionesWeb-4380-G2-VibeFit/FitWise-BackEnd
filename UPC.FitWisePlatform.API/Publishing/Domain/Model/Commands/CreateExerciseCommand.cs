namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record CreateExerciseCommand(
    int HealthPlanId,
    string Name,
    string Description,
    string Image);