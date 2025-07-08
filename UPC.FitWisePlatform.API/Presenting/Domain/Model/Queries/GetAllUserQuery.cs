namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;

public record GetAllUserQuery(string? EmailValue = null, int? ProfileId = null);