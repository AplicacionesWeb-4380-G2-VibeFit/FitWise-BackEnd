using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record CreateUserResource(
    string FirstName,
    string LastName,
    string Email,
    string BirthDate,
    string Gender,
    string Image,
    string AboutMe,
    int ProfileId);