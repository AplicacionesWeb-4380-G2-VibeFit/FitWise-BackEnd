using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewCommentQueryService
{
    Task<IEnumerable<ReviewComment>> Handle(GetAllReviewCommentsQuery query);
    Task<IEnumerable<ReviewComment>> Handle(GetReviewCommentsByReviewIdQuery query);
}