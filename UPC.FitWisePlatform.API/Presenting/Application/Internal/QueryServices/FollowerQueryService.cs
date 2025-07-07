using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.QueryServices;

public class FollowerQueryService(
    IFollowerRepository followerRepository) : IFollowerQueryService
{
    public async Task<Follower?> Handle(GetFollowerByIdQuery query)
    {
        return await followerRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Follower>> Handle(GetAllFollowerQuery query)
    {
        return await followerRepository.ListAsync();
    }
}