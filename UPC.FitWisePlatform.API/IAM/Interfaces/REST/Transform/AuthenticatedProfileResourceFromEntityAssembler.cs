using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedProfileResourceFromEntityAssembler
{
    public static AuthenticatedProfileResource ToResourceFromEntity(
        Profile profile, string token)
    {
        return new AuthenticatedProfileResource(profile.Id, profile.Username, token);
    }
}