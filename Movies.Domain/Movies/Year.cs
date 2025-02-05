namespace Movies.Domain.Movies;

public sealed record Year
{
    public const int MinYear = 1900;

    public static int MaxYear => DateTime.Today.Year;

    public int Value { get; init; }

    private Year()
    {
    }

    public static Year Create(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, MinYear);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, MaxYear);

        return new Year
        {
            Value = value
        };
    }
}
