using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IHealthPlanExerciseQueryService
{
    Task<HealthPlanExercise?> Handle(GetHealthPlanExerciseByIdQuery query);
    Task<IEnumerable<HealthPlanExercise>> Handle(GetHealthPlanExercisesByHealthPlanIdQuery query);
}