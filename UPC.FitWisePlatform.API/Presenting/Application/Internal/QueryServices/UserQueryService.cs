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
        return await userRepository.ListAsync();
    }
}