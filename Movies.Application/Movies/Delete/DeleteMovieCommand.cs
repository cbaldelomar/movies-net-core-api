using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Delete;

public record DeleteMovieCommand(Guid Id) : ICommand<bool>;

