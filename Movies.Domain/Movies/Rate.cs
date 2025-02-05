namespace Movies.Domain.Movies;

public sealed record Rate
{
    public const int MinRate = 0;
    public const int MaxRate = 10;

    public decimal Value { get; init; }

    private Rate()
    {
    }

    public static Rate Create(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinRate);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxRate);

        return new Rate
        {
            Value = value,
        };
    }
}
