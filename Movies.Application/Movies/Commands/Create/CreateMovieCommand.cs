using ErrorOr;
using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Commands.Create;

public sealed record CreateMovieCommand(
    string Title,
    int Year,
    string Director,
    short Duration,
    string? Poster,
    decimal? Rate,
    IEnumerable<string> Genres)
    : ICommand<ErrorOr<MovieResult>>;
