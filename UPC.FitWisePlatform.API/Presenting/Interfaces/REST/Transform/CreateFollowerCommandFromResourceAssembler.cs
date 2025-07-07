using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class CreateFollowerCommandFromResourceAssembler
{
    public static CreateFollowerCommand ToCommandFromResource(CreateFollowerResource resource)
    {
        return new CreateFollowerCommand(
            resource.FollowerUserId,
            resource.FollowedUserId);
    }
}