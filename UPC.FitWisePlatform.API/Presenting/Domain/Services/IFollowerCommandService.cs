using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface IFollowerCommandService
{
    Task<Follower?> Handle(CreateFollowerCommand command);

    Task<bool> Handle(DeleteFollowerCommand command);
}