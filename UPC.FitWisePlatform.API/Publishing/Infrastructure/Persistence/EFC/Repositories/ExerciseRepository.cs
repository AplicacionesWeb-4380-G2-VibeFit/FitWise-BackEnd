using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class ExerciseRepository(AppDbContext context) :
    BaseRepository<Exercise>(context), IExerciseRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Set<Exercise>().AnyAsync(e => e.Name == name);
    }
}