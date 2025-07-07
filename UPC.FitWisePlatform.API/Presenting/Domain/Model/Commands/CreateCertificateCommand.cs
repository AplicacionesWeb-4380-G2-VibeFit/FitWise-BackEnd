using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;

public record CreateCertificateCommand(
    int UserId,
    string Institution,
    DateObtained DateObtained,
    string Description,
    Status Status,
    CertificateCode CertificateCode,
    int YearsOfWork
);