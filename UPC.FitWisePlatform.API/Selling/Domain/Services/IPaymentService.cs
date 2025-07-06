using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Selling.Domain.Services;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> GetPendingPaymentsByUserAsync(string userId);
    Task<IEnumerable<Payment>> GetAllAsync();

}