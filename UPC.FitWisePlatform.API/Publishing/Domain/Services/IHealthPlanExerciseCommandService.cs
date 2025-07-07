using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanExerciseCommandService
{
    Task<HealthPlanExercise?> Handle(AssignExerciseToHealthPlanCommand command);
    Task<HealthPlanExercise?> Handle(UpdateHealthPlanExerciseCommand command);
    Task<bool> Handle(DeleteHealthPlanExerciseCommand command);
}