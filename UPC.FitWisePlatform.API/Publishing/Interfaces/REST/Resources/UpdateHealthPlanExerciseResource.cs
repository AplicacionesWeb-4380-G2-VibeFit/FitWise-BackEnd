namespace UPC.FitWisePlatform.API.Publishing.Interfaces.REST.Resources;

public record UpdateHealthPlanExerciseResource(int HealthPlanId, int ExerciseId, string DayOfWeek, string Instructions,
    int? Sets = null, int? Reps = null, int? DurationInMinutes = null);