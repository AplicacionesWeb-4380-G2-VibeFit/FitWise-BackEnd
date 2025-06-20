using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewReportQueryService
{
    Task<IEnumerable<ReviewReport>> Handle(GetAllReviewReportsQuery query);
    Task<IEnumerable<ReviewReport>> Handle(GetReviewReportsByReviewIdQuery query);
}