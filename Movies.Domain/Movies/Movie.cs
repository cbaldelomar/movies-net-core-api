using Movies.Domain.Abstractions;
using Movies.Domain.Genres;

namespace Movies.Domain.Movies;

public sealed class Movie : AggregateRoot<MovieId>
{
    private readonly List<Genre> _genres = [];

    public Title Title { get; private set; } = default!;

    public Year Year { get; private set; } = default!;

    public Director Director { get; private set; } = default!;

    public Duration Duration { get; private set; } = default!;

    public Url? Poster { get; private set; }

    public Rate? Rate { get; private set; }

    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();

    private Movie()
    {
    }

    public static Movie Create(
        MovieId id,
        Title title,
        Year year,
        Director director,
        Duration duration,
        Url? poster,
        Rate? rate,
        IEnumerable<Genre> genres)
    {
        Movie movie = new()
        {
            Id = id,
            Title = title,
            Year = year,
            Director = director,
            Duration = duration,
            Poster = poster,
            Rate = rate
        };

        movie.AddGenres(genres);

        return movie;
    }

    public void Update(
        Title title,
        Year year,
        Director director,
        Duration duration,
        Url? poster,
        Rate? rate,
        IEnumerable<Genre> genres)
    {
        Title = title;
        Year = year;
        Director = director;
        Duration = duration;
        Poster = poster;
        Rate = rate;

        UpdateGenres(genres);
    }

    private void UpdateGenres(IEnumerable<Genre> genres)
    {
        var newGenres = genres.Distinct().ToList();

        var deletedGenres = _genres.Except(newGenres).ToList();
        var addedGenres = newGenres.Except(_genres);

        foreach (var item in deletedGenres)
        {
            _genres.Remove(item);
        }


        AddGenres(addedGenres);
    }

    private void AddGenres(IEnumerable<Genre> genres)
    {
        foreach (Genre item in genres)
        {
            _genres.Add(item);
        }
    }
}
