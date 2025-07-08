namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record DateObtained
{
    public string DateObtainedValue { get; private set;  }

    public DateObtained(string dateObtainedValue)
    {
        if (string.IsNullOrWhiteSpace(dateObtainedValue))
            throw new ArgumentException("La fecha de nacimiento no puede estar vacía.", nameof(dateObtainedValue));

        if (!DateTime.TryParseExact(dateObtainedValue, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            throw new ArgumentException("Formato de fecha inválido. Use MM/DD/YYYY.", nameof(dateObtainedValue));

        DateObtainedValue = dateObtainedValue;
    }
    
    // Constructor sin parámetros requerido por EF Core (opcional pero recomendado)
    public DateObtained() { }

    public override string ToString() => DateObtainedValue;
}