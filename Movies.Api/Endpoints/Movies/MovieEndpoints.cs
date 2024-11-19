using MediatR;
using Movies.Application.Movies;
using Movies.Application.Movies.Queries.GetList;

namespace Movies.Api.Endpoints.Movies;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder route = app.MapGroup("movies");

        route.MapGet("/",
            async (
                [AsParameters] GetMovieListRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetMovieListQuery(request.Genre);

                IEnumerable<MovieResult> result = await sender.Send(query, cancellationToken);

                var response = result.Select(r => new MovieResponse(r)).ToList();

                return Results.Ok(response);
            });
    }
}
