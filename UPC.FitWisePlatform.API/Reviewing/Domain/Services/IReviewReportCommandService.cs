using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewReportCommandService
{
    Task<ReviewReport> Handle(CreateReviewReportCommand command);
    Task<ReviewReport?> Handle(UpdateReviewReportStatusCommand command);
}