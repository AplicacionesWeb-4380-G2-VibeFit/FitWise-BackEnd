using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Repositories;

public class ReviewRepository(AppDbContext context) : BaseRepository<Review>(context), IReviewRepository
{
    public async Task<IEnumerable<Review>> FindByHealthPlanIdAsync(int healthPlanId)
    {
        return await Context.Set<Review>()
            .Where(r => r.HealthPlanId == healthPlanId)
            .ToListAsync();
    }

    public new async Task<Review?> FindByIdAsync(int id)
    {
        return await Context.Set<Review>()
            .Include(r => r.Comments)
            .Include(r => r.Reports)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public new async Task<IEnumerable<Review>> ListAsync()
    {
        return await Context.Set<Review>()
            .Include(r => r.Comments)
            .Include(r => r.Reports)
            .ToListAsync();
    }
}