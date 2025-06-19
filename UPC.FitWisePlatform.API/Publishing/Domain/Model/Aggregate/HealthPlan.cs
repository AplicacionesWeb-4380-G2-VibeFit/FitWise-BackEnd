using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public partial class HealthPlan
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Objective { get; private set; }
    public Price Price { get; private set; } // Value Object
    public Duration Duration { get; private set; } // Value Object
    public string Description { get; private set; }
    public int CreatorId { get; private set; } // Foreign Key to Creator (can be null)
    
    // Colección de Meals para esta HealthPlan
    public ICollection<Meal> Meals { get; }
    
    // Colección de Exercises para esta HealthPlan
    public ICollection<Exercise> Exercises { get; }

    public HealthPlan()
    {
        this.Name = string.Empty;
        this.Objective = string.Empty;
        this.Price = new Price();
        this.Duration = new Duration();
        this.Description = string.Empty;
        this.CreatorId = 0;
        this.Meals = new List<Meal>(); // Inicializar la colección
        this.Exercises = new List<Exercise>();
    }

    public HealthPlan(string name, string objective, int amount, string currency, int value, string unit,
        string description, int creatorId)
    {
        this.Name = name;
        this.Objective = objective;
        this.Price = new Price(amount, currency);
        this.Duration = new Duration(value, unit);
        this.Description = description;
        this.CreatorId = creatorId;
        this.Meals = new List<Meal>(); // Inicializar la colección
        this.Exercises = new List<Exercise>();
    }
}