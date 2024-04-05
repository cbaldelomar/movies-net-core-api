using FastEndpoints;

namespace Movies.Api.Endpoints.Movies;

// https://fast-endpoints.com/docs/swagger-support#swagger-documentation
public class GetMovieListSummary : Summary<GetMovieList, GetMovieListRequest>
{
    public GetMovieListSummary()
    {
        var movies = new MovieResponse[]
        {
            MovieResponse.Example,
            MovieResponse.Example
        };

        var example = new GetMovieListResponse(movies);

        Summary = "Get movie list, optionally filtered by genre";
        ResponseExamples[200] = example;
        RequestParam(r => r.Genre, "Genre name");
    }
}
