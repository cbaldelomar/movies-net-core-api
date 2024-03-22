using Movies.Application.Abstractions;

namespace Movies.Application.Movies.GetList;

public sealed record GetMovieListQuery(string? Genre) : IQuery<IEnumerable<MovieResult>>;
