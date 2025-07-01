using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IExerciseQueryService
{
    Task<Exercise?> Handle(GetExerciseByIdQuery query);
}