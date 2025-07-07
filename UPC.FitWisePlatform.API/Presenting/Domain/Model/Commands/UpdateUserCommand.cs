using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;

public record UpdateUserCommand(
    int Id,
    string FirstName,
    string LastName,
    Email Email,
    BirthDate BirthDate,
    Gender Gender,
    string Username,
    string Password,
    Image Image,
    string AboutMe);