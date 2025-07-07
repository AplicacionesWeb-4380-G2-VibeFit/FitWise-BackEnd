namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record CreateExerciseCommand(string Name, string Description, Uri ImageUri);