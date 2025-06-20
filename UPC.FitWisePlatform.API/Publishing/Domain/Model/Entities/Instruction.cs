using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

public partial class Instruction
{
    public int Id { get; }
    public int Position { get; private set; } // Podría ser int para el orden de la instrucción
    public string Description { get; private set; }
    public int MealId { get; private set; } // Clave foránea para Meal
    
    // Propiedad de navegación para la relación con Meal
    public Meal Meal { get; internal set; }

    public Instruction()
    {
        this.Position = 0;
        this.Description = string.Empty;
        this.MealId = 0;
    }

    public Instruction(int position, string description, int mealId)
    {
        this.Position = position;
        this.Description = description;
        this.MealId = mealId;
    }
}