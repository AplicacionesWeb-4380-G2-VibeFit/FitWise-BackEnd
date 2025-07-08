using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Services;

public interface ICertificateCommandService
{
    Task<Certificate?> Handle(CreateCertificateCommand command);
    Task<Certificate?> Handle(UpdateCertificateCommand command);
    Task<bool> Handle(DeleteCertificateCommand command);
    
}