namespace UPC.FitWisePlatform.API.Presenting.Interfaces.REST.Resources;

public record CertificateResource(
    int Id,
    int UserId,
    string Institution,
    string DateObtained,
    string Description,
    string Status,
    string CertificateCode,
    int YearsOfWork);