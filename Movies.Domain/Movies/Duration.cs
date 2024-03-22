namespace Movies.Domain.Movies;

public sealed record Duration
{
    private const short MinimumDuration = 1;

    public short Value { get; init; }

    private Duration()
    {
    }

    public static Duration Create(short value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinimumDuration);

        return new Duration
        {
            Value = value,
        };
    }
}
