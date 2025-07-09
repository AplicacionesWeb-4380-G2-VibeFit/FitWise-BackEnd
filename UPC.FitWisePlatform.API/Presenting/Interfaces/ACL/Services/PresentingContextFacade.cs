using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.ACL.Services;

public class PresentingContextFacade(
    IUserCommandService  userCommandService,
    IUserQueryService userQueryService) : IPresentingContextFacade
{
    public async Task<int> CreateUser(string firstName, string lastName, string email, string birthDate, string gender,
        string image, string aboutMe, int profileId)
    {
        var emailCommand = new Email(email);
        var birthDateCommand = new BirthDate(birthDate);
        if (!Enum.TryParse(gender, true, out Gender genderCommand))
        {
            throw new ArgumentException("Gender is not valid");
        }

        var imageCommand = new Image(image);
        var createUserCommand =
            new CreateUserCommand(firstName, lastName, emailCommand, birthDateCommand, genderCommand, imageCommand,
                aboutMe, profileId);
        
        var createdUser = await userCommandService.Handle(createUserCommand);
        
        // Si createdUser no es null, significa que el usuario fue creado exitosamente
        if (createdUser != null)
        {
            // Puedes devolver el ID
            return createdUser.Id;
            // O si tu IPresentingContextFacade fuera Task<User?>, devolverías createdUser directamente
            // return createdUser;
        }
        else
        {
            // Manejar el caso en que la creación del usuario falló y el comando devolvió null.
            // Esto podría significar que el comando de usuario encontró una duplicidad, etc.
            // Puedes lanzar una excepción específica o devolver null.
            return 0;
        }
    }
}