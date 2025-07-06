using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace UPC.FitWisePlatform.API.Selling.Infrastructure.Persistence.EFC.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> ListAsync()
    {
        return await _context.Set<Payment>().ToListAsync();
    }

    public async Task<Payment?> FindByIdAsync(int id)
    {
        return await _context.Set<Payment>().FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> FindByOwnerIdAndStatusAsync(string ownerId, string status)
    {
        return await _context.Set<Payment>()
            .Where(p => p.OwnerId == ownerId && p.Status == status)
            .ToListAsync();
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _context.Set<Payment>().AddAsync(payment);
        await _context.SaveChangesAsync();

        // Manualmente obtener el último registro insertado por los mismos datos
        var inserted = await _context.Set<Payment>()
            .Where(p =>
                p.OwnerId == payment.OwnerId &&
                p.PlanId == payment.PlanId &&
                p.Amount == payment.Amount &&
                p.PaymentDate == payment.PaymentDate)
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();

        return inserted ?? payment; // Devuelve el objeto con ID válido
    }



    public void Update(Payment payment)
    {
        _context.Set<Payment>().Update(payment);
        _context.SaveChanges(); // ← también es necesario
    }

    public void Remove(Payment payment)
    {
        _context.Set<Payment>().Remove(payment);
        _context.SaveChanges(); // ← también necesario
    }
}