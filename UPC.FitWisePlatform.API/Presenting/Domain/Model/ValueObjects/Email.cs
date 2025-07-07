namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record Email
{
    public string EmailValue { get; private set; }
    
    public Email () { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El correo electrónico no puede estar vacío.", nameof(value));

        // Validación simple de formato de email
        try
        {
            var addr = new System.Net.Mail.MailAddress(value);
            if (addr.Address != value)
                throw new ArgumentException("Formato de correo electrónico inválido.", nameof(value));
        }
        catch
        {
            throw new ArgumentException("Formato de correo electrónico inválido.", nameof(value));
        }

        EmailValue = value;
    }
    

    public override string ToString() => EmailValue;
}