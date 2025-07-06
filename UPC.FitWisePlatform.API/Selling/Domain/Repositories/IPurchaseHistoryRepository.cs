using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Repositories
{
    public interface IPurchaseHistoryRepository
    {
        Task<PurchaseHistory?> FindByUserIdAsync(string userId);
        Task<PurchaseHistory?> FindByIdWithPaymentsAsync(string userId); // ✅ nuevo
        Task AddAsync(PurchaseHistory history);
        void Update(PurchaseHistory history);
        Task<IEnumerable<PurchaseHistory>> ListAsync();
    
    }
}