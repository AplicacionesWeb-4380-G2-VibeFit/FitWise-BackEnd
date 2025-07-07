using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewCommandService
{
    Task<Review> Handle(CreateReviewCommand command);
    Task<Review?> Handle(UpdateReviewCommand command);
    Task<bool> Handle(DeleteReviewCommand command);
}