namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record BirthDate
{
    public string BirthDateValue { get; private set;  }

    public BirthDate(string birthDateValue)
    {
        if (string.IsNullOrWhiteSpace(birthDateValue))
            throw new ArgumentException("La fecha de nacimiento no puede estar vacía.", nameof(birthDateValue));

        if (!DateTime.TryParseExact(birthDateValue, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out var birthDate))
            throw new ArgumentException("Formato de fecha inválido. Use MM/DD/YYYY.", nameof(birthDateValue));

        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;

        if (age < 18)
            throw new ArgumentException("La persona debe ser mayor de 18 años.", nameof(birthDateValue));
        
        BirthDateValue = birthDateValue;
    }
    
    // Constructor sin parámetros requerido por EF Core (opcional pero recomendado)
    public BirthDate() { }

    public override string ToString() => BirthDateValue;
}