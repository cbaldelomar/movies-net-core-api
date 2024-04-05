using Movies.Application.Movies;

namespace Movies.Api.Endpoints.Movies;

public class MovieResponse
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

    public static readonly MovieResponse Example = new()
    {
        Id = Guid.Empty,
        Title = "My Movie",
        Year = DateTime.Now.Year,
        Director = "John Doe",
        Duration = 123,
        Poster = "example.com/poster.jpg",
        Rate = 9.5m,
        Genres = new string[] { "Drama", "Action" },
    };
}
