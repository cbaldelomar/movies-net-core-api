namespace Movies.Api.Endpoints.Movies;

public record CreateMovieRequest(
    string Title,
    int Year,
    string Director,
    short Duration,
    string? Poster,
    decimal? Rate,
    IEnumerable<string> Genres);
