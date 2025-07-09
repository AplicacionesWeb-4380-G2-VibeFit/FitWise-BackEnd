using UPC.FitWisePlatform.API.IAM.Domain.Model.Commands;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Queries;
using UPC.FitWisePlatform.API.IAM.Domain.Services;

namespace UPC.FitWisePlatform.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : IIamContextFacade
{
    /*public async Task<int> CreateUser(string username, string password)
    {
        var signUpCommand = new SignUpCommand(username, password);
        await profileCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetProfileByUsernameQuery(username);
        var result = await profileQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }*/

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetProfileByUsernameQuery(username);
        var result = await profileQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetProfileByIdQuery(userId);
        var result = await profileQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}