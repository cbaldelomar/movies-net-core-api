namespace Movies.Domain.Movies;

public sealed record Rate
{
    private const int MINIMUM_RATE = 0;
    private const int MAXIMUM_RATE = 10;

    public decimal Value { get; init; }

    private Rate()
    {
    }

    public static Rate Create(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MINIMUM_RATE);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MAXIMUM_RATE);

        return new Rate
        {
            Value = value,
        };
    }
}
