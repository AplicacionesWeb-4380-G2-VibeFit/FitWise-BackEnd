namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

public record Duration(int Value, string Unit)
{
    public Duration( ): this(0, string.Empty) {}
}