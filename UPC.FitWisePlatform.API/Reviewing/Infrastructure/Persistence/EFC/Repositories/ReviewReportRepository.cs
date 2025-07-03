using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Repositories;

public class ReviewReportRepository(AppDbContext context) : BaseRepository<ReviewReport>(context), IReviewReportRepository
{
    public async Task<IEnumerable<ReviewReport>> FindByReviewIdAsync(int reviewId)
    {
        return await Context.Set<ReviewReport>()
            .Where(rp => rp.ReviewId == reviewId)
            .ToListAsync();
    }
}