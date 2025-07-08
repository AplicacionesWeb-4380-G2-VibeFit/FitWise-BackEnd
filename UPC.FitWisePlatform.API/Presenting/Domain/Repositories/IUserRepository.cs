using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(Email email);
    
}