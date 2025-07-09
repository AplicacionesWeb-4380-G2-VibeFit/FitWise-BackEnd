namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record HealthPlanExerciseResource(int Id, int HealthPlanId, int ExerciseId, string DayOfWeek,
    string Instructions, int? Sets, int? Reps, int? DurationInMinutes);