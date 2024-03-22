namespace Movies.Api.Endpoints.Movies;

public record GetMovieListResponse(IEnumerable<MovieResponse> Movies);
