using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Repositories;

public interface IPurchasedPlanRepository
{
    Task<IEnumerable<PurchasedPlan>> ListAsync();
    Task<PurchasedPlan?> FindByIdAsync(int id);
    Task AddAsync(PurchasedPlan plan);
    void Update(PurchasedPlan plan);
    void Remove(PurchasedPlan plan);
}