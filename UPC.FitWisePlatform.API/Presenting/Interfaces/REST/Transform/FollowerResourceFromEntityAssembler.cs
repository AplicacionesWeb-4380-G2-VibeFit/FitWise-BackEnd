using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class FollowerResourceFromEntityAssembler
{
    public static FollowerResource ToResourceFromEntity(Follower entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), 
                "Cannot convert a null Follower entity to a FollowerResource.");
        }
        
        return new FollowerResource(
            entity.Id,
            entity.FollowerUserId,
            entity.FollowedUserId);
    }
}