using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Publishing.Domain.Services;

namespace UPC.FitWisePlatform.API.Publishing.Application.Internal.QueryServices;

public class ExerciseQueryService(
    IExerciseRepository exerciseRepository) : IExerciseQueryService
{
    public async Task<Exercise?> Handle(GetExerciseByIdQuery query)
    {
        return await exerciseRepository.FindByIdAsync(query.Id);
    }
}