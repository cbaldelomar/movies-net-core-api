namespace Movies.Domain.Movies;

public sealed record Duration
{
    public const short MinDuration = 1;
    public const short MaxDuration = short.MaxValue;

    public short Value { get; init; }

    private Duration()
    {
    }

    public static Duration Create(short value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinDuration);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxDuration);

        return new Duration
        {
            Value = value,
        };
    }
}
