using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Reviewing.Domain.Repositories;
using UPC.FitWisePlatform.API.Reviewing.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Reviewing.Application.Internal.CommandServices;

public class ReviewCommentCommandService(IReviewCommentRepository commentRepository, IUnitOfWork unitOfWork) : IReviewCommentCommandService
{
    public async Task<ReviewComment> Handle(CreateReviewCommentCommand command)
    {
        var comment = new ReviewComment(command.ReviewId, command.UserId, command.Content);
        await commentRepository.AddAsync(comment);
        await unitOfWork.CompleteAsync();
        return comment;
    }

    public async Task<ReviewComment?> Handle(UpdateReviewCommentCommand command)
    {
        var comment = await commentRepository.FindByIdAsync(command.Id);
        if (comment == null) return null;

        comment.Update(command.Content);
        commentRepository.Update(comment);
        await unitOfWork.CompleteAsync();
        return comment;
    }

    public async Task<bool> Handle(DeleteReviewCommentCommand command)
    {
        var comment = await commentRepository.FindByIdAsync(command.Id);
        if (comment == null) return false;

        commentRepository.Remove(comment);
        await unitOfWork.CompleteAsync();
        return true;
    }
}