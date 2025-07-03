using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;

public interface IReviewReportRepository : IBaseRepository<ReviewReport>
{
    Task<IEnumerable<ReviewReport>> FindByReviewIdAsync(int reviewId);
}