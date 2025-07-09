namespace UPC.FitWisePlatform.API.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes the username and password to sign up
 * </remarks>
 */
public record SignUpCommand(string Username, string Password, string FirstName,
    string LastName,
    string Email,
    string BirthDate,
    string Gender,
    string Image,
    string AboutMe);