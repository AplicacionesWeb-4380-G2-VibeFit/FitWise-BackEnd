using UPC.FitWisePlatform.API.Presenting.Interfaces.ACL;

namespace UPC.FitWisePlatform.API.IAM.Application.Internal.OutboundServices.Services;

public class PresentingExternalService(
    IPresentingContextFacade presentingContextFacade) : IPresentingExternalService
{
    public async Task<int> CreatePresentingUserAsync(string firstName,
        string lastName,
        string email,
        string birthDate,
        string gender,
        string image,
        string aboutMe,
        int profileId)
    {
        int presentingUserId = await presentingContextFacade.CreateUser( // Ya no es int?, se que devuelve int
            firstName, lastName, email, birthDate, gender, image, aboutMe, profileId);
        
        // <--- ¡CORRECCIÓN AQUÍ: Manejo explícito si la creación falla (ID es 0)! --->
        if (presentingUserId == 0) // Asumiendo que 0 es un ID inválido/indicador de fallo
        {
            throw new InvalidOperationException("Presenting user creation failed. The returned ID was 0.");
        }

        return presentingUserId; // Retorna directamente el int
    }
}