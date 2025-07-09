namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record Image
{
    public string Url { get; private set; }
    
    public Image () {}

    public Image(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("La URL de la imagen no puede estar vacía.", nameof(url));

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
            !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            throw new ArgumentException("La URL de la imagen no es válida.", nameof(url));

        Url = url;
    }

    public override string ToString() => Url;
}