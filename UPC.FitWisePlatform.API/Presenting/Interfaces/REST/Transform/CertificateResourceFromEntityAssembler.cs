using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Transform;

public static class CertificateResourceFromEntityAssembler
{
    public static CertificateResource ToResourceFromEntity(Certificate entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), 
                "Cannot convert a null Certificate entity to a CertificateResource.");
        }
        
        return new CertificateResource(
            entity.Id,
            entity.UserId,
            entity.Institution,
            entity.DateObtained.DateObtainedValue,
            entity.Description,
            entity.Status.ToString(),
            entity.CertificateCode.CodeValue,
            entity.YearsOfWork);
        
        
    }
}