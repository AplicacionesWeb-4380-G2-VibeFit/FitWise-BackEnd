using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public partial class HealthPlanMeal
{
    public int Id { get; }
    public int HealthPlanId { get; private set; }
    public int MealId { get; private set; }
    public DayOfWeekType DayOfWeek { get; private set; } 
    public MealTime MealTime { get; private set; }  
    public string Notes { get; private set; }
    
    public UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate.HealthPlan HealthPlan { get; private set; }
    public Meal Meal { get; private set; }

    public HealthPlanMeal() {}

    public HealthPlanMeal(int healthPlanId, int mealId, DayOfWeekType dayOfWeek, MealTime mealTime, string notes)
    {
        if (healthPlanId <= 0)
            throw new ArgumentOutOfRangeException(nameof(healthPlanId), "HealthPlan ID must be greater than zero.");
        if (mealId <= 0)
            throw new ArgumentOutOfRangeException(nameof(mealId), "Meal ID must be greater than zero.");
        
        // Enum validation: Ensure the enum value is defined and not the 'Unknown' default
        if (!Enum.IsDefined(dayOfWeek) || dayOfWeek == DayOfWeekType.Unknown)
            throw new ArgumentOutOfRangeException(nameof(dayOfWeek), "The day of the week for the meal is not a valid value.");
        if (!Enum.IsDefined(mealTime) || mealTime == MealTime.Unknown)
            throw new ArgumentOutOfRangeException(nameof(mealTime), "The meal time is not a valid value.");
        
        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentNullException(nameof(notes), "The notes cannot be null or empty.");
        
        this.HealthPlanId = healthPlanId;
        this.MealId = mealId;
        this.DayOfWeek = dayOfWeek;
        this.MealTime = mealTime;
        this.Notes = notes;
    }
    
    public void UpdateDetails(DayOfWeekType newDayOfWeek, MealTime newMealTime, string newNotes)
    {
        if (!Enum.IsDefined(newDayOfWeek) || newDayOfWeek == DayOfWeekType.Unknown)
            throw new ArgumentOutOfRangeException(nameof(newDayOfWeek), "The new day of the week for the meal is not a valid value.");
        if (!Enum.IsDefined(newMealTime) || newMealTime == MealTime.Unknown)
            throw new ArgumentOutOfRangeException(nameof(newMealTime), "The new meal time is not a valid value.");
        
        if (string.IsNullOrWhiteSpace(newNotes))
            throw new ArgumentNullException(nameof(newNotes), "The new notes cannot be null or empty.");

        this.DayOfWeek = newDayOfWeek;
        this.MealTime = newMealTime;
        this.Notes = newNotes;
    }
}