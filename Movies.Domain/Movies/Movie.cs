using Movies.Domain.Abstractions;
using Movies.Domain.Genres;

namespace Movies.Domain.Movies;

public sealed class Movie : AuditableEntity<byte[]>, IAggregateRoot
{
    private readonly List<Genre> _genres = [];

    public MovieId UUID { get; } = default!;

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
        Title title,
        Year year,
        Director director,
        Duration duration,
        Url? poster,
        Rate? rate,
        IEnumerable<Genre> genres)
    {
        //MovieId id = new(Guid.NewGuid());

        Movie movie = new()
        {
            //Id = id,
            Title = title,
            Year = year,
            Director = director,
            Duration = duration,
            Poster = poster,
            Rate = rate
        };

        movie.UpdateGenres(genres);

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

    public void UpdateGenres(IEnumerable<Genre> genres)
    {
        var newGenres = genres.Distinct().ToList();

        var deleteGenres = _genres.Except(newGenres);
        var addGenres = newGenres.Except(_genres);

        foreach (var item in deleteGenres)
        {
            _genres.Remove(item);
        }

        foreach (var item in addGenres)
        {
            _genres.Add(item);
        }
    }
}
