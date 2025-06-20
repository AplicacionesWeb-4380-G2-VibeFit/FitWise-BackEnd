using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.CommandServices;

public class ReviewCommandService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork) : IReviewCommandService
{
    public async Task<Review> Handle(CreateReviewCommand command)
    {
        var review = new Review(command.UserId, command.Score, command.Description, command.HealthPlanId);
        await reviewRepository.AddAsync(review);
        await unitOfWork.CompleteAsync();
        return review;
    }

    public async Task<Review?> Handle(UpdateReviewCommand command)
    {
        var review = await reviewRepository.FindByIdAsync(command.Id);
        if (review == null) return null;

        review.Update(command.Score, command.Description);
        reviewRepository.Update(review);
        await unitOfWork.CompleteAsync();
        return review;
    }

    public async Task<bool> Handle(DeleteReviewCommand command)
    {
        var review = await reviewRepository.FindByIdAsync(command.Id);
        if (review == null) return false;

        reviewRepository.Remove(review);
        await unitOfWork.CompleteAsync();
        return true;
    }
}