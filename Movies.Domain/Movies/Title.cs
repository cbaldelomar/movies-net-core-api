namespace Movies.Domain.Movies;

public sealed record Title
{
    public const int MaxLength = 100;

    public string Value { get; init; } = string.Empty;

    private Title()
    {
    }

    public static Title Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, MaxLength);

        return new Title
        {
            Value = value
        };
    }
}
