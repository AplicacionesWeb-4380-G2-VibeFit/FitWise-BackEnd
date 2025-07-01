using UPC.FitWisePlatform.API.Publishing.Domain.Model.Aggregate;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Repositories;

public interface IHealthPlanRepository : IBaseRepository<HealthPlan>
{
    Task<IEnumerable<HealthPlan>> FindByProfileId(int profileId);
    Task<bool> ExistsByPlanNameAsync(string planName);
}