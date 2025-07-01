namespace UPC.FitWisePlatform.API.Publishing.Domain.Model.ValueObjects;

public record Duration
{
    public int DurationValue { get; private set; }
    public DurationUnit DurationType { get; private set; }
    
    public Duration() { }

    public Duration(int durationValue, DurationUnit durationType)
    {
        if (durationValue <= 0)
        {
            throw new ArgumentException("Duration value must be positive and integer.", nameof(durationValue));
        }
        if (durationType == DurationUnit.Undefined)
        {
            throw new ArgumentException("Duration unit cannot be undefined.", nameof(durationType));
        }
        this.DurationValue = durationValue;
        this.DurationType = durationType;
    }
    
    public override string ToString()
    {
        var unitString = this.DurationType.ToString();
        if (this.DurationValue > 1)
        {
            unitString = unitString switch
            {
                "Day" => "Days",
                "Week" => "Weeks",
                "Month" => "Months",
                "Year" => "Years",
                _ => unitString
            };
        }
        return $"{this.DurationValue} {unitString}";
    }
}