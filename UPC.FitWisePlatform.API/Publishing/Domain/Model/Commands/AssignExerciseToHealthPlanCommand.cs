using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record AssignExerciseToHealthPlanCommand(int HealthPlanId, int ExerciseId, DayOfWeekType DayOfWeek,
    string Instructions, int? Sets = null, int? Reps = null, int? DurationMinutes = null);