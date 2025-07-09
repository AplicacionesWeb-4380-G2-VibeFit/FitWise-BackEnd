namespace UPC.FitWisePlatform.API.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, string Password, string FirstName,
    string LastName,
    string Email,
    string BirthDate,
    string Gender,
    string Image,
    string AboutMe);