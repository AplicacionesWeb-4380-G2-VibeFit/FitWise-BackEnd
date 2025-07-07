using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.QueryServices;

public class ReviewQueryService(IReviewRepository reviewRepository) : IReviewQueryService
{
    public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query)
    {
        return await reviewRepository.ListAsync();
    }

    public async Task<Review?> Handle(GetReviewByIdQuery query)
    {
        return await reviewRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Review>> Handle(GetReviewsByHealthPlanIdQuery query)
    {
        return await reviewRepository.FindByHealthPlanIdAsync(query.HealthPlanId);
    }
}