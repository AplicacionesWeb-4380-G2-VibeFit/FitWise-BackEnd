using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Application.Internal.QueryServices;

public class PaymentQueryService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentQueryService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await _paymentRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetPendingPaymentsByUserIdAsync(string ownerId)
    {
        return await _paymentRepository.FindByOwnerIdAndStatusAsync(ownerId, "pending");
    }
    

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _paymentRepository.ListAsync();
    }
}