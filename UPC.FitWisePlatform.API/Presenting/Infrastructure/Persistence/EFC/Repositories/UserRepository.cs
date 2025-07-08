using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) :
    BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> ExistsByProfileIdAsync(int profileId)
    {
        return await Context.Set<User>().AnyAsync(us => us.ProfileId == profileId);
    }

    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await Context.Set<User>().AnyAsync(us => us.Email.EmailValue == email.EmailValue);
    }
}