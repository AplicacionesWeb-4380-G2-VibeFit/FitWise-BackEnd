using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.QueryServices;

public class UserQueryService(
    IUserRepository userRepository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUserQuery query)
    {
        var users = await userRepository.ListAsync();
        if (!string.IsNullOrWhiteSpace(query.EmailValue))
            users = users.Where(u => u.Email.EmailValue == query.EmailValue);
        if (query.ProfileId.HasValue)
            users = users.Where(u => u.ProfileId == query.ProfileId.Value);
        return users;
    }
}