namespace Movies.Domain.Abstractions;

public interface IDateTimeProvider
{
    public DateTime Now { get; }

    public DateTime UtcNow { get; }

    public DateTime Today { get; }
}
