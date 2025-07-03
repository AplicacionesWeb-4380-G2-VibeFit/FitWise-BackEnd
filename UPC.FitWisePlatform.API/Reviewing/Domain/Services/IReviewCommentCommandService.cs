using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Services;

public interface IReviewCommentCommandService
{
    Task<ReviewComment> Handle(CreateReviewCommentCommand command);
    Task<ReviewComment?> Handle(UpdateReviewCommentCommand command);
    Task<bool> Handle(DeleteReviewCommentCommand command);
}