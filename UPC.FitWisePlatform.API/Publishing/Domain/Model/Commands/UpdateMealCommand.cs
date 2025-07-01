namespace Nuevo_FitWise_Ordenado.Publishing.Domain.Model.Commands;

public record UpdateMealCommand(int Id, string Name, string Description, Uri ImageUri);