namespace Movies.Domain.Movies;

public sealed record Url
{
    public string Value { get; init; } = string.Empty;

    private Url()
    {
    }

    public static Url Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (!IsValidUrl(value))
        {
            throw new ArgumentException($"{value} is not a valid url.", nameof(value));
        }

        return new Url
        {
            Value = value
        };
    }

    private static bool IsValidUrl(string url)
    {
        // Check if the URL is well-formed
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
        {
            return false;
        }

        // Check if the URL scheme is HTTP or HTTPS
        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        {
            return false;
        }

        // Additional validation logic can be added here if needed

        return true;
    }
}
