using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class MealRepository(AppDbContext context) :
    BaseRepository<Meal>(context), IMealRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Set<Meal>().AnyAsync(m => m.Name == name);
    }
}