using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewQueryService
{
    Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query);
    Task<Review?> Handle(GetReviewByIdQuery query);
    Task<IEnumerable<Review>> Handle(GetReviewsByHealthPlanIdQuery query);
}