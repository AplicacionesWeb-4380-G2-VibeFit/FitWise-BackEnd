// PurchasedPlanCommandService.cs
using UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Selling.Domain.Services;

public class PurchasedPlanCommandService : IPurchasedPlanService
{
    private readonly IPurchasedPlanRepository _repository;

    public PurchasedPlanCommandService(IPurchasedPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PurchasedPlan>> GetAllAsync() => await _repository.ListAsync();

    public async Task<PurchasedPlan?> GetByIdAsync(int id) => await _repository.FindByIdAsync(id);

    public async Task<int> CreateAsync(CreatePurchasedPlanCommand command)
    {
        var plan = new PurchasedPlan
        {
            OwnerId = command.OwnerId,
            PlanId = command.PlanId,
            PurchaseDate = command.PurchaseDate,
            Status = command.Status
        };

        await _repository.AddAsync(plan);
        return plan.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdatePurchasedPlanCommand command)
    {
        var plan = await _repository.FindByIdAsync(id);
        if (plan is null) return false;

        plan.OwnerId = command.OwnerId;
        plan.PlanId = command.PlanId;
        plan.PurchaseDate = command.PurchaseDate;
        plan.Status = command.Status;

        _repository.Update(plan);
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var plan = await _repository.FindByIdAsync(id);
        if (plan is null) return false;

        _repository.Remove(plan); // Ya guarda internamente
        return true;
    }


}