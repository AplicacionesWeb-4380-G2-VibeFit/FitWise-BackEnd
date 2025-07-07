using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Infrastructure.Persistence.EFC.Repositories;

public class PurchasedPlanRepository(AppDbContext context)
    : BaseRepository<PurchasedPlan>(context), IPurchasedPlanRepository
{
    public async Task<IEnumerable<PurchasedPlan>> FindByOwnerIdAsync(string ownerId)
    {
        return await Context.Set<PurchasedPlan>()
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task AddAsync(PurchasedPlan plan)
    {
        await Context.Set<PurchasedPlan>().AddAsync(plan);
        await Context.SaveChangesAsync();
    }

    public void Update(PurchasedPlan plan)
    {
        Context.Set<PurchasedPlan>().Update(plan);
        Context.SaveChanges(); // ✅ Aquí estaba el problema
    }

    public void Remove(PurchasedPlan plan)
    {
        Context.Set<PurchasedPlan>().Remove(plan);
        Context.SaveChanges();
    }
}
