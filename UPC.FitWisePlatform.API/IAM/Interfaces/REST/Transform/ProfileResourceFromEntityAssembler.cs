using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile profile)
    {
        return new ProfileResource(profile.Id, profile.Username);
    }
}