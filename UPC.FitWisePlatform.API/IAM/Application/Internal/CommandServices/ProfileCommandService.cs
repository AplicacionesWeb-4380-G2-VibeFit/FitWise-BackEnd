using UPC.FitWisePlatform.API.IAM.Application.Internal.OutboundServices;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Commands;
using UPC.FitWisePlatform.API.IAM.Domain.Repositories;
using UPC.FitWisePlatform.API.IAM.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class ProfileCommandService(
    IProfileRepository profileRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IPresentingExternalService presentingExternalService)
    : IProfileCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(Profile user, string token)> Handle(SignInCommand command)
    {
        var user = await profileRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        if (profileRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var profile = new Profile(command.Username, hashedPassword);
        
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            await presentingExternalService.CreatePresentingUserAsync(command.FirstName, command.LastName, 
                command.Email, command.BirthDate, command.Gender, command.Image, command.AboutMe, profile.Id);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}