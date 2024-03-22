using Movies.Domain.Abstractions;
using Movies.Domain.Movies;

namespace Movies.Domain.Genres;

public sealed class Genre : Entity<int>, IAggregateRoot
{
    private readonly List<Movie> _movies = [];

    public string Name { get; private set; } = string.Empty;

    public IReadOnlyList<Movie> Movies => _movies.AsReadOnly();

    private Genre()
    {
    }

    public static Genre Create(string name)
    {
        return new Genre()
        {
            Name = name
        };
    }
}
