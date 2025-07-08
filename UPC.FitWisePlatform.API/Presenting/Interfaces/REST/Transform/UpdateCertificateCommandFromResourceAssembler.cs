using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class UpdateCertificateCommandFromResourceAssembler
{
    public static UpdateCertificateCommand ToCommandFromResource(int id, UpdateCertificateResource resource)
    {
        if (!Enum.TryParse(resource.Status, true, out Status status))
        {
            throw new ArgumentException($"Invalid Status value: {resource.Status}");
        }

        return new UpdateCertificateCommand(
            id,
            resource.UserId,
            resource.Institution,
            new DateObtained(resource.DateObtained),
            resource.Description,
            status,
            new CertificateCode(resource.CertificateCode),
            resource.YearsOfWork);  
    }
}