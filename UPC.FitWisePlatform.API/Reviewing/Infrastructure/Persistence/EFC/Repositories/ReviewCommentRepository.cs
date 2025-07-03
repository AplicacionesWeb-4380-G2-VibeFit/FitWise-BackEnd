using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Infrastructure.Persistence.EFC.Repositories;

public class ReviewCommentRepository(AppDbContext context) : BaseRepository<ReviewComment>(context), IReviewCommentRepository
{
    public async Task<IEnumerable<ReviewComment>> FindByReviewIdAsync(int reviewId)
    {
        return await Context.Set<ReviewComment>()
            .Where(c => c.ReviewId == reviewId)
            .ToListAsync();
    }
}