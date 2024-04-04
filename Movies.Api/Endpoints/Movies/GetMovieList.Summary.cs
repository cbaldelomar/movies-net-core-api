using FastEndpoints;

namespace Movies.Api.Endpoints.Movies;

// https://fast-endpoints.com/docs/swagger-support#swagger-documentation
public class GetMovieListSummary : Summary<GetMovieList, GetMovieListRequest>
{
    public GetMovieListSummary()
    {
        var movies = new MovieResponse[]
        {
            new() {
                Id = Guid.Empty,
                Title = "Movie 1",
                Year = DateTime.Now.Year,
                Director = "John Doe",
                Duration = 123,
                Poster = "example.com/poster.jpg",
                Rate = 9.5m,
                Genres = new string[] { "Drama", "Action" },

            },
            new()
            {
                Id = Guid.Empty,
                Title = "Movie 2",
                Year = DateTime.Now.Year,
                Director = "John Doe",
                Duration = 150,
                Poster = "example.com/poster2.jpg",
                Rate = 9.5m,
                Genres = new string[] { "Crime", "Action" },

            }
        };

        var example = new GetMovieListResponse(movies);

        Summary = "Returns movie list, optionally filtered by genre";
        Response(200, "json object with movie list", example: example);
        RequestParam(r => r.Genre, "Genre name");
    }
}
