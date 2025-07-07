using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Services;

public interface IPurchasedPlanService
{
    Task<IEnumerable<PurchasedPlan>> GetAllAsync();
    Task<PurchasedPlan?> GetByIdAsync(int id);
}