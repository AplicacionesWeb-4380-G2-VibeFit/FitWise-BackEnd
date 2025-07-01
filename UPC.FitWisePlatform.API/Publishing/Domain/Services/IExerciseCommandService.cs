using UPC.FitWisePlatform.API.Publishing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Services;

public interface IExerciseCommandService
{
    Task<Exercise?> Handle(CreateExerciseCommand command);
    Task<Exercise?> Handle(UpdateExerciseCommand command);
}