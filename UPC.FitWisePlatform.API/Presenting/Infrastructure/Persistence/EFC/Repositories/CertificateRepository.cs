using Microsoft.EntityFrameworkCore;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using UPC.FitWisePlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Infrastructure.Persistence.EFC.Repositories;

public class CertificateRepository(AppDbContext context) :
    BaseRepository<Certificate>(context), ICertificateRepository
{
    public Task<bool> ExistsByCertificateCodeAndUserIdAsync(CertificateCode certificateCode, int userId)
    {
        return Context.Set<Certificate>()
            .AnyAsync(c => c.CertificateCode.CodeValue == certificateCode.CodeValue && c.UserId == userId);
    }
}