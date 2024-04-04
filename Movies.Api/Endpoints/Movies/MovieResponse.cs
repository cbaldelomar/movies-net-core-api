using Movies.Application.Movies;

namespace Movies.Api.Endpoints.Movies;

public record MovieResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Director { get; init; } = string.Empty;
    public short Duration { get; init; }
    public string? Poster { get; init; }
    public decimal? Rate { get; init; }
    public IEnumerable<string> Genres { get; init; } = [];

    public MovieResponse()
    {

    }

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
