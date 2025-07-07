using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record UpdateUserResource(
    string FirstName,
    string LastName,
    string Email,
    string BirthDate,
    string Gender,
    string Username,
    string Password,
    string Image,
    string AboutMe);