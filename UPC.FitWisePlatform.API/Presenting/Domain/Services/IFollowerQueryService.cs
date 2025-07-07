using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface IFollowerQueryService
{
    Task<Follower?> Handle(GetFollowerByIdQuery query);
    Task<IEnumerable<Follower>> Handle(GetAllFollowerQuery query);
}