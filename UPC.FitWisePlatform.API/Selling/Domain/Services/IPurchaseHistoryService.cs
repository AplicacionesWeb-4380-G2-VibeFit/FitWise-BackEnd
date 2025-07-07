using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Services;

public interface IPurchaseHistoryService
{
    Task<PurchaseHistory?> GetByUserIdAsync(string userId);
    Task AddPaymentAsync(string userId, Payment payment);
}