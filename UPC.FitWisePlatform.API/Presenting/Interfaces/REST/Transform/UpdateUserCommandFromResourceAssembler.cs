using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource)
    {
        if (!Enum.TryParse(resource.Gender, true, out Gender gender))
        {
            throw new ArgumentException($"Invalid Gender value: {resource.Gender}");
        }
        
        return new UpdateUserCommand(
            id, 
            resource.FirstName,
            resource.LastName,
            new Email(resource.Email),
            new BirthDate(resource.BirthDate),
            gender,
            resource.Username,
            resource.Password,
            new Image(resource.Image),
            resource.AboutMe);
    }
    
}