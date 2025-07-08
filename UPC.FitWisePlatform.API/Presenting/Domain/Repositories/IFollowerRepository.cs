using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;
namespace UPC.FitWisePlatform.API.Presenting.Domain.Repositories;

public interface IFollowerRepository: IBaseRepository<Follower>
{
    Task<bool> ExistsByFollowerUserIdAndFollowedUserIdAsync(
        int followerUserId, 
        int followedUserId);
}