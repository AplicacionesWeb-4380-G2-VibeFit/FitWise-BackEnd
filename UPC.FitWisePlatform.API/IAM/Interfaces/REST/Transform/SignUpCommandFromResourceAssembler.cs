using UPC.FitWisePlatform.API.IAM.Domain.Model.Commands;
using UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password, resource.FirstName, resource.LastName,
            resource.Email, resource.BirthDate, resource.Gender, resource.Image, resource.AboutMe);
    }
}