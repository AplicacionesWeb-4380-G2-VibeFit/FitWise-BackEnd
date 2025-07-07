using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;

public class PurchaseHistoryQueryService
{
    private readonly IPurchaseHistoryRepository _repository;

    public PurchaseHistoryQueryService(IPurchaseHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PurchaseHistory?> GetByUserIdAsync(string userId)
    {
        return await _repository.FindByUserIdAsync(userId);
    }
    public async Task<IEnumerable<PurchaseHistory>> GetAllAsync()
    {
        return await _repository.ListAsync();
    }

}