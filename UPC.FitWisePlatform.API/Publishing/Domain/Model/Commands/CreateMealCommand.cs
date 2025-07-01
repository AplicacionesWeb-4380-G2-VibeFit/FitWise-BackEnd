namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record CreateMealCommand(string Name, string Description, Uri ImageUri);