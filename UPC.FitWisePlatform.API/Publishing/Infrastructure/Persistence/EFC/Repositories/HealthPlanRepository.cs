using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Publishing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Infrastructure.Persistence.EFC.Repositories;

public class HealthPlanRepository(AppDbContext context) :
    BaseRepository<HealthPlan>(context), IHealthPlanRepository
{
    public async Task<IEnumerable<HealthPlan>> FindByProfileId(int profileId)
    {
        return await Context.Set<HealthPlan>().Where(p => p.ProfileId == profileId).ToListAsync();
    }

    public async Task<bool> ExistsByPlanNameAsync(string planName)
    {
        return await Context.Set<HealthPlan>().AnyAsync(hp => hp.PlanName == planName);
    }
}