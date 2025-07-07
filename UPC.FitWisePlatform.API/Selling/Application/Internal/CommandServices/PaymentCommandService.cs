using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Selling.Domain.Services;

namespace UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;

public class PaymentCommandService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentCommandService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    public async Task<bool> PatchAsync(int id, string newStatus)
    {
        var payment = await _paymentRepository.FindByIdAsync(id);
        if (payment is null) return false;

        payment.Status = newStatus;
        _paymentRepository.Update(payment);
        return true;
    }

    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await _paymentRepository.ListAsync();
    }
    public async Task<IEnumerable<Payment>> GetPendingPaymentsByUserAsync(string userId)
    {
        var all = await _paymentRepository.ListAsync();
        return all.Where(p => p.OwnerId == userId && p.Status == "pending");
    }

public async Task<int> CreateAsync(CreatePaymentCommand command)
{
    var payment = new Payment
    {
        OwnerId = command.OwnerId,
        PlanId = command.PlanId,
        Amount = command.Amount,
        Currency = command.Currency,
        Method = command.Method,
        Status = command.Status,
        PaymentDate = command.PaymentDate,
        PurchasedPlanId = command.PurchasedPlanId
    };

    var savedPayment = await _paymentRepository.AddAsync(payment);
    return savedPayment.Id;
}



    public async Task UpdateAsync(UpdatePaymentCommand command)
    {
        var payment = await _paymentRepository.FindByIdAsync(command.Id);
        if (payment is null) return;

        payment.Status = command.Status;
        _paymentRepository.Update(payment);
    }

    public async Task<bool> DeleteAsync(DeletePaymentCommand command)
    {
        var payment = await _paymentRepository.FindByIdAsync(command.Id);
        if (payment is null) return false;

        _paymentRepository.Remove(payment);
        return true;
    }

}