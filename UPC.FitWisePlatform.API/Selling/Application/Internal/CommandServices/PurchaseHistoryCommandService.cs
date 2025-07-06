using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Application.Internal.CommandServices;

public class PurchaseHistoryCommandService
{
    private readonly IPurchaseHistoryRepository _repository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseHistoryCommandService(IPurchaseHistoryRepository repository, IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PurchaseHistory> CreateAsync(CreatePurchaseHistoryCommand command)
    {
        var newHistory = new PurchaseHistory
        {
            Id = command.UserId,
            Payments = new List<Payment>()
        };

        await _repository.AddAsync(newHistory);
        await _unitOfWork.CompleteAsync();

        return newHistory;
    }

    public async Task<PurchaseHistory?> AddPaymentToHistoryAsync(string userId, AddPaymentToHistoryCommand command)
    {
        var history = await _repository.FindByIdWithPaymentsAsync(userId);
        if (history == null) return null;

        var payment = await _paymentRepository.FindByIdAsync(command.PaymentId);
        if (payment == null) return null;

        history.Payments.Add(payment);


        await _unitOfWork.CompleteAsync();

        return history;
    }
}