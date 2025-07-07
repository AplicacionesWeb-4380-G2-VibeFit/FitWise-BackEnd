using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.QueryServices;

public class CertificateQueryService(
    ICertificateRepository certificateRepository) : ICertificateQueryService
{
    public async Task<Certificate?> Handle(GetCertificateByIdQuery query)
    {
        return await certificateRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Certificate>> Handle(GetAllCertificateQuery query)
    {
        return await certificateRepository.ListAsync();
    }
}