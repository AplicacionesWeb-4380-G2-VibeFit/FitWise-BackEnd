using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Selling.Infrastructure.Persistence.EFC.Repositories;

public class PurchaseHistoryRepository(AppDbContext context)
    : BaseRepository<PurchaseHistory>(context), IPurchaseHistoryRepository
{
    public async Task<PurchaseHistory?> FindByUserIdAsync(string userId)
    {
        return await Context.Set<PurchaseHistory>()
            .Include(ph => ph.Payments)
            .FirstOrDefaultAsync(ph => ph.Id == userId);
    }
    public async Task<PurchaseHistory?> FindByIdWithPaymentsAsync(string id)
    {
        return await Context.Set<PurchaseHistory>()
            .Include(ph => ph.Payments)
            .FirstOrDefaultAsync(ph => ph.Id == id);
    }
    public async Task<IEnumerable<PurchaseHistory>> ListAsync()
    {
        return await Context.Set<PurchaseHistory>()
            .Include(ph => ph.Payments)
            .ToListAsync();
        
    }

}