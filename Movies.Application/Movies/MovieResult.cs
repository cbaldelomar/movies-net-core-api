namespace Movies.Application.Movies;

public sealed record MovieResult(
    Guid Id,
    string Title,
    int Year,
    string Director,
    short Duration,
    string? Poster,
    decimal? Rate,
    IEnumerable<string> Genres);
