using UPC.FitWisePlatform.API.Presenting.Domain.Model.Commands;
using UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;
using UPC.FitWisePlatform.API.Presenting.Domain.Repositories;
using UPC.FitWisePlatform.API.Presenting.Domain.Services;
using UPC.FitWisePlatform.API.Shared.Domain.Repositories;

namespace UPC.FitWisePlatform.API.Presenting.Application.Internal.CommandServices;

public class CertificateCommandService(ICertificateRepository certificateRepository,
    IUnitOfWork unitOfWork) : ICertificateCommandService
{
    public async Task<Certificate?> Handle(CreateCertificateCommand command)
    {
        if (await certificateRepository.ExistsByCertificateCodeAndUserIdAsync(
                command.CertificateCode, command.UserId))
            throw new Exception("Certificate with the same certificate code and user id already exists");
        var certificate = new Certificate(
            command.UserId,
            command.Institution,
            command.DateObtained,
            command.Description,
            command.Status,
            command.CertificateCode,
            command.YearsOfWork
        );
        await certificateRepository.AddAsync(certificate);
        await unitOfWork.CompleteAsync();
        
        return certificate;
    }

    public async Task<Certificate?> Handle(UpdateCertificateCommand command)
    {
        var certificate = await certificateRepository.FindByIdAsync(command.Id);

        if (certificate == null)
            throw new Exception($"Certificate with id '{command.Id}' does not exist");

        // Verifica duplicidad de código de certificado para el usuario, excluyendo el actual
        if (await certificateRepository.ExistsByCertificateCodeAndUserIdAsync(command.CertificateCode, command.UserId)
            && (certificate.CertificateCode != command.CertificateCode || certificate.UserId != command.UserId))
            throw new Exception("Another certificate with the same certificate code and user id already exists");

        certificate.UpdateDetails(
            command.Institution,
            command.DateObtained,
            command.Description,
            command.Status,
            command.CertificateCode,
            command.YearsOfWork
        );

        certificateRepository.Update(certificate);
        await unitOfWork.CompleteAsync();

        return certificate;
    }

    public async Task<bool> Handle(DeleteCertificateCommand command)
    {
        var certificate = await certificateRepository.FindByIdAsync(command.Id);
        if (certificate == null)
            throw new Exception($"Certificate with id '{command.Id}' does not exist");

        certificateRepository.Remove(certificate);
        await unitOfWork.CompleteAsync();
        return true;
    }
}