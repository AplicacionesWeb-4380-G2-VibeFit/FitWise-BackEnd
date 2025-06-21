using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

public partial class Ingredient
{
    public int Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; } // Podría incluir cantidad y unidad aquí
    public int MealId { get; private set; } // Clave foránea para Meal

    // Propiedad de navegación para la relación con Meal
    public Meal Meal { get; internal set; }

    public Ingredient()
    {
        this.Name = string.Empty;
        this.Description = string.Empty;
        this.MealId = 0;
    }

    public Ingredient(string name, string description, int mealId)
    {
        this.Name = name;
        this.Description = description;
        this.MealId = mealId;
    }
}