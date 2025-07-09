using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.ACL;

public interface IPresentingContextFacade
{
    Task<int> CreateUser(string firstName,
        string lastName,
        string email,
        string birthDate,
        string gender,
        string image,
        string aboutMe,
        int profileId);
}