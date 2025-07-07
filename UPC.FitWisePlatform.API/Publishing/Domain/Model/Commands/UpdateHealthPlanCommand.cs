using UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

public record UpdateHealthPlanCommand(int Id, string PlanName, string Objective, Duration Duration, Price Price, 
    string Description);