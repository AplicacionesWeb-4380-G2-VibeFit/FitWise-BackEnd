using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record CreateHealthPlanCommand(string PlanName, string Objective, Duration Duration, Price Price, 
    string Description, int ProfileId);