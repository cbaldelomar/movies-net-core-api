using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Update;

public sealed record UpdateMovieCommand(
    Guid Id,
    string? Title,
    int? Year,
    string? Director,
    short? Duration,
    string? Poster,
    decimal? Rate,
    IEnumerable<string>? Genres)
    : ICommand<bool>;
