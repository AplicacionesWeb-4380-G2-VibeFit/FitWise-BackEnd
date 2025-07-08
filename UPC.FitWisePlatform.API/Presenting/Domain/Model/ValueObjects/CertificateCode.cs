namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record CertificateCode
{
    public string CodeValue { get; private set; }

    public CertificateCode() { }

    public CertificateCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de certificado no puede estar vacío.", nameof(value));

        // Validación de formato: SIGLAS-AAAA-MMDD-XXXX
        var regex = new System.Text.RegularExpressions.Regex(@"^[A-Z]{3,6}-\d{4}-\d{4}-[A-Z0-9]{4}$");
        if (!regex.IsMatch(value))
            throw new ArgumentException("Formato de código de certificado inválido.", nameof(value));

        CodeValue = value;
    }

    public override string ToString() => CodeValue;
}