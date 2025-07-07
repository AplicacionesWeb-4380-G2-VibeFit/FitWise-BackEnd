using UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

public partial class Certificate
{
    public int Id { get; }
    
    public int UserId { get; private set; }
    
    public string Institution { get; private set; }
    
    public DateObtained DateObtained { get; private set; }
    
    public string Description { get; private set; }
    
    public Status Status { get; private set; }
    
    public CertificateCode CertificateCode { get; private set; }
    
    public int YearsOfWork { get; private set; }
    
    public Certificate() {}
    
    public Certificate(
        int userId,
        string institution,
        DateObtained dateObtained,
        string description,
        Status status,
        CertificateCode certificateCode,
        int yearsOfWork)
    {
        // Enum validation
        if (!Enum.IsDefined(status) || status == Status.Unknown)
            throw new ArgumentOutOfRangeException(nameof(status), "The status is not a valid value.");
        
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero.");
        
        if (string.IsNullOrWhiteSpace(institution))
            throw new ArgumentNullException(nameof(institution), "Institution cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(institution))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        
        if (yearsOfWork <= 0)
            throw new ArgumentOutOfRangeException(nameof(yearsOfWork), "YearsOfWork must be greater than zero.");
        
        
        this.UserId = userId;
        this.Institution = institution;
        this.DateObtained = dateObtained;
        this.Description = description;
        this.Status = status;
        this.CertificateCode = certificateCode;
        this.YearsOfWork = yearsOfWork;
    }
    
    public void UpdateDetails(
        string institution,
        DateObtained dateObtained,
        string description,
        Status status,
        CertificateCode certificateCode,
        int yearsOfWork)
    {
        // Enum validation
        if (!Enum.IsDefined(status) || status == Status.Unknown)
            throw new ArgumentOutOfRangeException(nameof(status), "The status is not a valid value.");
        
        if (string.IsNullOrWhiteSpace(institution))
            throw new ArgumentNullException(nameof(institution), "Institution cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description), "Description cannot be null or empty.");
        
        if (yearsOfWork <= 0)
            throw new ArgumentOutOfRangeException(nameof(yearsOfWork), "YearsOfWork must be greater than zero.");
        
        this.Institution = institution;
        this.DateObtained = dateObtained;
        this.Description = description;
        this.Status = status;
        this.CertificateCode = certificateCode;
        this.YearsOfWork = yearsOfWork;
    }
}