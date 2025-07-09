namespace UPC.FitWisePlatform.API.IAM.Application.Internal.OutboundServices;

public interface IPresentingExternalService
{
    Task<int> CreatePresentingUserAsync(string firstName,
        string lastName,
        string email,
        string birthDate,
        string gender,
        string image,
        string aboutMe,
        int profileId);
}