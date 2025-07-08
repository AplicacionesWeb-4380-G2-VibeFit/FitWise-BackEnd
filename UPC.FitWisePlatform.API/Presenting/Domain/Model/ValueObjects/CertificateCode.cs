namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record CertificateCode
{
    public string CodeValue { get; private set; }

    public CertificateCode() { }

    public CertificateCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de certificado no puede estar vacío.", nameof(value));

        // Validación de formato: SIGLAS-AAAA-####-XXXX
        // SIGLAS = 2 a 10 letras (mayúsculas o minúsculas)
        // AAAA = año de emisión (4 dígitos)
        // #### = número de empleado, folio o ID único (4 dígitos)
        // XXXX = 4 letras o números (mayúsculas, minúsculas o dígitos)
        var regex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]{2,10}-\d{4}-\d{4}-[A-Za-z0-9]{4}$");if (!regex.IsMatch(value))
            throw new ArgumentException("Formato de código de certificado inválido.", nameof(value));

        CodeValue = value;
    }

    public override string ToString() => CodeValue;
}