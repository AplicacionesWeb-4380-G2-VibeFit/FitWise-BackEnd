using UPC.FitWisePlatform.API.IAM.Domain.Model.Commands;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}