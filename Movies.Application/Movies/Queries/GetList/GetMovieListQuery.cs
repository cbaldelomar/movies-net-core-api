using Movies.Application.Abstractions;

namespace Movies.Application.Movies.Queries.GetList;

public sealed record GetMovieListQuery(string? Genre) : IQuery<IEnumerable<MovieResult>>;
