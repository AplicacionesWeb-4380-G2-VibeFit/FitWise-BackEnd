using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Infrastructure.Persistence.EFC.Repositories;

public class FollowerRepository(AppDbContext context) :
    BaseRepository<Follower>(context), IFollowerRepository
{
    public async Task<bool> ExistsByFollowerUserIdAndFollowedUserIdAsync(int followerUserId, int followedUserId)
    {
        return await Context.Set<Follower>()
            .AnyAsync(fo => fo.FollowerUserId == followerUserId && 
                            fo.FollowedUserId == followedUserId);

    }
}