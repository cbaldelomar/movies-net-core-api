using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Queries.GetById;

public sealed record GetMovieByIdQuery(Guid Id) : IQuery<MovieResult>;
