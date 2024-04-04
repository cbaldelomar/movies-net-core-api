using Movies.Application.Abstractions;

namespace Movies.Application.Movies.GetById;

public sealed record GetMovieByIdQuery(Guid Id) : IQuery<MovieResult>;
