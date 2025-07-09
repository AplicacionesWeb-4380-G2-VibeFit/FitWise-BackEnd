using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.CommandServices;

public class FollowerCommandService(IFollowerRepository followerRepository,
    IUnitOfWork unitOfWork) : IFollowerCommandService
{
    public async Task<Follower?> Handle(CreateFollowerCommand command)
    {
        if (await followerRepository.ExistsByFollowerUserIdAndFollowedUserIdAsync(
                command.FollowerUserId, command.FollowedUserId))
            throw new Exception("User with the same FollowerUserId and FollowedUserId already exists");
        
        var follower = new Follower(
            command.FollowerUserId, command.FollowedUserId);
        
        await followerRepository.AddAsync(follower);
        await unitOfWork.CompleteAsync();
        
        return follower;
    }

    public async Task<bool> Handle(DeleteFollowerCommand command)
    {
        var follower = await followerRepository.FindByIdAsync(command.Id);
        if (follower == null)
            throw new Exception($"Follower with id '{command.Id}' does not exist");
        
        followerRepository.Remove(follower);
        await unitOfWork.CompleteAsync();
        return true;
    }
}