namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

public record Price
{
    public decimal PriceValue { get; private set; }
    public Currency Currency { get; private set; }
    
    public Price() { }

    public Price(decimal priceValue, Currency currency)
    {
        if (priceValue <= 0)
        {
            throw new ArgumentException("Price value cannot be negative or zero.", nameof(priceValue));
        }
        if (currency == Currency.Undefined)
        {
            throw new ArgumentException("Currency cannot be null, empty or undefined.", nameof(currency));
        }
        this.PriceValue = priceValue;
        this.Currency = currency;
    }
    
    public override string ToString()
    {
        var currencySymbol = GetCurrencySymbol(this.Currency);
        return $"{currencySymbol} {this.PriceValue:N2}";
    }
    
    private static string GetCurrencySymbol(Currency currency)
    {
        return currency switch
        {
            Currency.USD => "$",
            Currency.PEN => "S/",
            Currency.EUR => "€",
            Currency.GBP => "£",
            _ => currency.ToString()
        };
    }
}