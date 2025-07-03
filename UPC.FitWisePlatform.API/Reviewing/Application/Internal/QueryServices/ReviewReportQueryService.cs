using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.QueryServices;

public class ReviewReportQueryService(IReviewReportRepository reportRepository) : IReviewReportQueryService
{
    public async Task<IEnumerable<ReviewReport>> Handle(GetAllReviewReportsQuery query)
    {
        return await reportRepository.ListAsync();
    }

    public async Task<IEnumerable<ReviewReport>> Handle(GetReviewReportsByReviewIdQuery query)
    {
        return await reportRepository.FindByReviewIdAsync(query.ReviewId);
    }
}