namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public class UpdatePaymentCommand
{
    public int Id { get; set; } // ← CAMBIA init por set
    public string Status { get; set; } = string.Empty;
}
