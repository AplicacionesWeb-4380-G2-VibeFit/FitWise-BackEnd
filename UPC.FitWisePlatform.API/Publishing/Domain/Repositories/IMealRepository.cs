using UPC.FitWisePlatform.API.Publishing.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Publishing.Domain.Repositories;

public interface IMealRepository : IBaseRepository<Meal>
{
    Task<bool> ExistsByNameAsync(string name);
}