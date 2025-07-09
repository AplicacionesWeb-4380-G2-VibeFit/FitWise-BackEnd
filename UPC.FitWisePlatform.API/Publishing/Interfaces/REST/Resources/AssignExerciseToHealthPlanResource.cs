namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record AssignExerciseToHealthPlanResource(
    int HealthPlanId, int ExerciseId, string DayOfWeek, string Instructions, int? Sets = null, int? Reps = null, 
    int? DurationInMinutes = null);