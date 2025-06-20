using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class HealthPlanRepository(AppDbContext context) : 
    BaseRepository<HealthPlan>(context), IHealthPlanRepository
{
    public async Task<IEnumerable<HealthPlan>> FindByCreatorIdAsync(int creatorId)
    {
        return await Context.Set<HealthPlan>()
            .Where(healthPlan => healthPlan.CreatorId == creatorId)
            .ToListAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Set<HealthPlan>().AnyAsync(healthPlan => healthPlan.Name == name);
    }

    public new async Task<HealthPlan?> FindByIdAsync(int id)
    {
        return await Context.Set<HealthPlan>().FirstOrDefaultAsync(healthPlan => healthPlan.Id == id);
    }

    public new async Task<IEnumerable<HealthPlan>> ListAsync()
    {
        return await Context.Set<HealthPlan>().ToListAsync();
    }
}