using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.QueryServices;

public class ReviewCommentQueryService(IReviewCommentRepository commentRepository) : IReviewCommentQueryService
{
    public async Task<IEnumerable<ReviewComment>> Handle(GetAllReviewCommentsQuery query)
    {
        return await commentRepository.ListAsync();
    }

    public async Task<IEnumerable<ReviewComment>> Handle(GetReviewCommentsByReviewIdQuery query)
    {
        return await commentRepository.FindByReviewIdAsync(query.ReviewId);
    }
}