namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record UpdateExerciseCommand(int Id, string Name, string Description, Uri ImageUri);