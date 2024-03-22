namespace Movies.Domain.Movies;

public sealed record Year
{
    private const int MINIMUM_YEAR = 1900;
    private static readonly int s_maximumYear = DateTime.Today.Year;

    public int Value { get; init; }

    private Year()
    {
    }

    public static Year Create(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MINIMUM_YEAR);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, s_maximumYear);

        return new Year
        {
            Value = value
        };
    }
}
