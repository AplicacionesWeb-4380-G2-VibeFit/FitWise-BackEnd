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
        var certificates = await certificateRepository.ListAsync();
        if (query.UserId.HasValue)
            certificates = certificates.Where(c => c.UserId == query.UserId.Value);
        return certificates;
    }
}