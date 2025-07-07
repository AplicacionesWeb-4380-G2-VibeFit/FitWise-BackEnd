using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Repositories;

public interface ICertificateRepository: IBaseRepository<Certificate>
{
    Task<bool> ExistsByCertificateCodeAndUserIdAsync(CertificateCode certificateCode, int userId);
}