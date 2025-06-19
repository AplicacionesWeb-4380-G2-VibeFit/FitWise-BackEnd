using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public class Meal
{
    public int Id { get; }
    public int HealthPlanId { get; private set; } // Clave foránea para HealthPlan
    public Uri? Image { get; private set; }
    
    // Propiedad de navegación para la relación con HealthPlan
    public HealthPlan HealthPlan { get; internal set; }
    
    // Colección de Instructions para esta Meal
    public ICollection<Instruction> Instructions { get; }
    
    public ICollection<Ingredient> Ingredients { get; }

    public Meal()
    {
        this.HealthPlanId = 0;
        this.Image = null;
        this.Instructions = new List<Instruction>();
        this.Ingredients = new List<Ingredient>();
    }

    public Meal(int healthPlanId, string image)
    {
        this.HealthPlanId = healthPlanId;
        this.Image = new Uri(image);
        this.Instructions = new List<Instruction>();
        this.Ingredients = new List<Ingredient>();
    }
}