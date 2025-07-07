using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Queries;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface ICertificateQueryService
{
    Task<Certificate?> Handle(GetCertificateByIdQuery query);
    Task<IEnumerable<Certificate>> Handle(GetAllCertificateQuery query);
}