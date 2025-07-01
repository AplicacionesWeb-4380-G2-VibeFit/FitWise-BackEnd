using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public partial class HealthPlan
{
    public int Id { get; }
    public string PlanName { get; private set; }
    public string Objective { get; private set; }
    public Duration Duration { get; private set; } 
    public Price Price { get; private set; }
    public string Description { get; private set; }
    public int ProfileId { get; private set; }
    
    public ICollection<HealthPlanMeal> HealthPlanMeals { get; private set; } = new List<HealthPlanMeal>();
    public ICollection<HealthPlanExercise> HealthPlanExercises { get; private set; } = new List<HealthPlanExercise>();

    public HealthPlan() { }

    public HealthPlan(
        string planName, 
        string objective, 
        Duration duration, 
        Price price, 
        string description,
        int profileId)
    {
        if (string.IsNullOrWhiteSpace(planName))
            throw new ArgumentNullException(nameof(planName), "HealthPlan name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(objective))
            throw new ArgumentNullException(nameof(objective), "HealthPlan objective cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "HealthPlan description cannot be null or empty.");
        
        this.PlanName = planName;
        this.Objective = objective;
        this.Duration = duration;
        this.Price = price;
        this.Description = description;
        this.ProfileId = profileId;
    }

    public void UpdateDetails(string newPlanName, string newObjective, Duration newDuration, Price newPrice,
        string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newPlanName))
            throw new ArgumentNullException(nameof(newPlanName), "HealthPlan new name cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newObjective))
            throw new ArgumentNullException(nameof(newObjective), "HealthPlan new objective cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentNullException(nameof(newDescription), "HealthPlan new description cannot be null or empty.");
        
        PlanName = newPlanName;
        Description = newDescription;
        Objective = newObjective;
        Duration = newDuration;
        Price = newPrice;
    }
}