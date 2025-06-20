namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

public partial class Exercise
{
    public int Id { get; }
    public int HealthPlanId { get; private set; } // Clave foránea para HealthPlan
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Uri? Image { get; private set; }
    
    // Propiedad de navegación para la relación con HealthPlan
    public HealthPlan HealthPlan { get; internal set; }

    public Exercise()
    {
        this.HealthPlanId = 0;
        this.Name = string.Empty;
        this.Description = string.Empty;
        this.Image = null;
    }

    public Exercise(int healthPlanId, string name, string description, string image)
    {
        this.HealthPlanId = healthPlanId;
        this.Name = name;
        this.Description = description;
        this.Image = new Uri(image);
    }
}