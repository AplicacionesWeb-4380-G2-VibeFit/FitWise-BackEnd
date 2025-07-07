namespace UPC.FitWisePlatform.API.Selling.Domain.Model.Commands;

public class DeletePaymentCommand
{
    public int Id { get; set; }

    public DeletePaymentCommand() { }

    public DeletePaymentCommand(int id)
    {
        Id = id;
    }
}
