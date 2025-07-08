namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.ValueObjects;

public record Image
{
    public string Url { get; }
    
    public Image () {}

    public Image(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("La URL de la imagen no puede estar vacía.", nameof(url));

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
            !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            throw new ArgumentException("La URL de la imagen no es válida.", nameof(url));

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        if (!allowedExtensions.Any(ext => uriResult.AbsolutePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))
            throw new ArgumentException("La URL debe apuntar a una imagen (.jpg, .jpeg, .png, .gif, .bmp, .webp).", nameof(url));

        Url = url;
    }

    public override string ToString() => Url;
}