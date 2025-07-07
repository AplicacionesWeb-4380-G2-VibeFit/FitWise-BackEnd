namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record UpdateCertificateResource(
    int UserId,
    string Institution,
    string DateObtained,
    string Description,
    string Status,
    string CertificateCode,
    int YearsOfWork);