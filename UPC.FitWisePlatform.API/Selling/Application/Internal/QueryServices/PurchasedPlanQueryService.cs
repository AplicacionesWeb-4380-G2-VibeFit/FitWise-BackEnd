using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;

public class PurchasedPlanQueryService
{
    private readonly IPurchasedPlanRepository _repository;

    public PurchasedPlanQueryService(IPurchasedPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PurchasedPlan>> GetAllAsync() => await _repository.ListAsync();

    public async Task<PurchasedPlan?> GetByIdAsync(int id) => await _repository.FindByIdAsync(id);
}