using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), 
                "Cannot convert a null User entity to a UserResource.");
        }
        
        return new UserResource(
            entity.Id, 
            entity.FirstName, 
            entity.LastName, 
            entity.Email.EmailValue, 
            entity.BirthDate.BirthDateValue,
            entity.Gender.ToString(),
            entity.Image.Url,
            entity.AboutMe,
            entity.ProfileId);
    }
    
}