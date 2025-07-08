namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record BirthDate
{
    public string BirthDateValue { get; private set;  }

    public BirthDate(string birthDateValue)
    {
        if (string.IsNullOrWhiteSpace(birthDateValue))
            throw new ArgumentException("La fecha de nacimiento no puede estar vacía.", nameof(birthDateValue));

        if (!DateTime.TryParseExact(birthDateValue, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            throw new ArgumentException("Formato de fecha inválido. Use MM/DD/YYYY.", nameof(birthDateValue));

        BirthDateValue = birthDateValue;
    }
    
    // Constructor sin parámetros requerido por EF Core (opcional pero recomendado)
    public BirthDate() { }

    public override string ToString() => BirthDateValue;
}