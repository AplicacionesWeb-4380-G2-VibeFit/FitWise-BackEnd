using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Selling.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Selling.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
namespace UPC.FitWisePlatform.API.Selling.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> ListAsync();
        Task<Payment?> FindByIdAsync(int id);
        Task<IEnumerable<Payment>> FindByOwnerIdAndStatusAsync(string ownerId, string status);
        Task<Payment> AddAsync(Payment payment); // Antes: Task AddAsync(...)
        void Update(Payment payment);
        void Remove(Payment payment);
        
    }
}
