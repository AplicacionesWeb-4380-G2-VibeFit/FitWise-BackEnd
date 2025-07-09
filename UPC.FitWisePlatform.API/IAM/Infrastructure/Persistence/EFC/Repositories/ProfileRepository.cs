using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.IAM.Domain.Model.Aggregates;
using UPC.FitWisePlatform.API.IAM.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/**
 * <summary>
 *     The user repository
 * </summary>
 * <remarks>
 *     This repository is used to manage users
 * </remarks>
 */
public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    /**
     * <summary>
     *     Find a user by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>The user</returns>
     */
    public async Task<Profile?> FindByUsernameAsync(string username)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    /**
     * <summary>
     *     Check if a user exists by username
     * </summary>
     * <param name="username">The username to search</param>
     * <returns>True if the user exists, false otherwise</returns>
     */
    public bool ExistsByUsername(string username)
    {
        return Context.Set<Profile>().Any(user => user.Username.Equals(username));
    }
}