namespace Movies.Domain.Movies;

public sealed record Director
{
    public const int MaxLength = 100;

    public string Value { get; init; } = string.Empty;

    private Director()
    {
    }

    public static Director Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLength);

        return new Director
        {
            Value = value
        };
    }
}
