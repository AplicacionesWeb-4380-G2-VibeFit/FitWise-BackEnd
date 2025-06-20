namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

public record Price(decimal Amount, string Currency)
{
    public Price() : this(0, string.Empty) {}
}