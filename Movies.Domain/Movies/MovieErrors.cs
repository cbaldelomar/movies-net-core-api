using Movies.Domain.Utilities;

namespace Movies.Domain.Movies;

internal static class MovieErrors
{
    public static Error NotFound(MovieId id) =>
        Error.NotFound(
            "Movie.NotFound",
            $"The movie with the Id = {id.Value} was not found");

    public static readonly ValidationError TitleMaxLength =
        Error.Validation(
            $"Movie.{nameof(TitleMaxLength)}",
            $"Title max length is {Title.MaxLength}");

    public static readonly ValidationError DirectorMaxLength =
        Error.Validation(
            $"Movie.{nameof(DirectorMaxLength)}",
            $"Director max length is {Director.MaxLength}");
}
