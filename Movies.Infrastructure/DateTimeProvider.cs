using Movies.Domain.Abstractions;

namespace Movies.Infrastructure;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Today => DateTime.Today;
}
