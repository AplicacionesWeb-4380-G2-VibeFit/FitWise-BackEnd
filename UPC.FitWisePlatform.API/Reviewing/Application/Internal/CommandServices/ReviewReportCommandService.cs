using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.CommandServices;

public class ReviewReportCommandService(IReviewReportRepository reportRepository, IUnitOfWork unitOfWork) : IReviewReportCommandService
{
    public async Task<ReviewReport> Handle(CreateReviewReportCommand command)
    {
        var report = new ReviewReport(command.ReviewId, command.UserId, command.Reason);
        await reportRepository.AddAsync(report);
        await unitOfWork.CompleteAsync();
        return report;
    }

    public async Task<ReviewReport?> Handle(UpdateReviewReportStatusCommand command)
    {
        var report = await reportRepository.FindByIdAsync(command.Id);
        if (report == null) return null;

        report.UpdateStatus(command.Status);
        reportRepository.Update(report);
        await unitOfWork.CompleteAsync();
        return report;
    }
}