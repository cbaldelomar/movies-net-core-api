using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Commands.Delete;

public record DeleteMovieCommand(Guid Id) : ICommand<bool>;

