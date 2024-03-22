using Movies.Application.Movies;

namespace Movies.Api.Endpoints.Movies;

public record MovieResponse
{
    public Guid Id { get; }
    public string Title { get; }
    public int Year { get; }
    public string Director { get; }
    public short Duration { get; }
    public string? Poster { get; }
    public decimal? Rate { get; }
    public IEnumerable<string> Genres { get; }

    public MovieResponse(MovieResult result)
    {
        Id = result.Id;
        Title = result.Title;
        Year = result.Year;
        Director = result.Director;
        Duration = result.Duration;
        Poster = result.Poster;
        Rate = result.Rate;
        Genres = result.Genres;
    }
}