using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public partial class HealthPlanExercise
{
    public int Id { get; }
    public int HealthPlanId { get; private set; }
    public int ExerciseId { get; private set; }
    public DayOfWeekType DayOfWeek { get; private set; }
    public int? Sets { get; private set; }
    public int? Reps { get; private set; }
    public int? DurationInMinutes { get; private set; }
    public string Instructions { get; private set; }
    
    public UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate.HealthPlan HealthPlan { get; private set; }
    public Exercise Exercise { get; private set; }
    
    public HealthPlanExercise() { }

    public HealthPlanExercise(int healthPlanId, int exerciseId, DayOfWeekType dayOfWeek, string instructions,
        int? sets = null , int? reps = null, int? durationInMinutes = null)
    {
        if (healthPlanId <= 0)
            throw new ArgumentOutOfRangeException(nameof(healthPlanId), "HealthPlan ID must be greater than zero.");
        if (exerciseId <= 0)
            throw new ArgumentOutOfRangeException(nameof(exerciseId), "Exercise ID must be greater than zero.");
        
        // Enum validation
        if (!Enum.IsDefined(dayOfWeek) || dayOfWeek == DayOfWeekType.Unknown)
            throw new ArgumentOutOfRangeException(nameof(dayOfWeek), "The day of the week for the exercise is not a valid value.");
        
        if (sets is <= 0)
            throw new ArgumentOutOfRangeException(nameof(sets), "The number of sets must be greater than zero if specified.");
        if (reps is <= 0)
            throw new ArgumentOutOfRangeException(nameof(reps), "The number of repetitions must be greater than zero if specified.");
        if (durationInMinutes is <= 0)
            throw new ArgumentOutOfRangeException(nameof(durationInMinutes), "The duration must be greater than zero if specified.");
        
        // Validación para instrucciones (si es necesario)
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Instructions cannot be empty or whitespace if specified.", nameof(instructions));
        
        this.HealthPlanId = healthPlanId;
        this.ExerciseId = exerciseId;
        this.DayOfWeek = dayOfWeek;
        this.Sets = sets;
        this.Reps = reps;
        this.DurationInMinutes = durationInMinutes;
        this.Instructions = instructions;
    }

    public void UpdateDetails(DayOfWeekType newDayOfWeek, string newInstructions, int? newSets = null, 
        int? newReps = null, int? newDurationInMinutes = null)
    {
        // Enum validation
        if (!Enum.IsDefined(newDayOfWeek) || newDayOfWeek == DayOfWeekType.Unknown)
            throw new ArgumentOutOfRangeException(nameof(newDayOfWeek), "The new day of the week for the exercise is not a valid value.");

        if (newSets is <= 0)
            throw new ArgumentOutOfRangeException(nameof(newSets), "The new number of sets must be greater than zero if specified.");
        if (newReps is <= 0)
            throw new ArgumentOutOfRangeException(nameof(newReps), "The new number of repetitions must be greater than zero if specified.");
        if (newDurationInMinutes is <= 0)
            throw new ArgumentOutOfRangeException(nameof(newDurationInMinutes), "The new duration of minutes must be greater than zero if specified.");
        
        if (string.IsNullOrWhiteSpace(newInstructions))
            throw new ArgumentException("New instructions cannot be empty or whitespace if specified.", nameof(newInstructions));

        this.DayOfWeek = newDayOfWeek;
        this.Sets = newSets;
        this.Reps = newReps;
        this.DurationInMinutes = newDurationInMinutes;
        this.Instructions = newInstructions;
    }
}