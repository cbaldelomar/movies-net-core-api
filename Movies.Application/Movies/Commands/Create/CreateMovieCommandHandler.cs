using ErrorOr;
using Movies.Application.Abstractions;
using Movies.Application.Movies.Services;
using Movies.Domain.Abstractions;
using Movies.Domain.Genres;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.Commands.Create;

public sealed class CreateMovieCommandHandler(
    ICheckMovieExistsQueryService queryService,
    IGenreRepository genreRepository,
    IMovieRepository movieRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovieCommand, ErrorOr<MovieResult>>
{
    private readonly ICheckMovieExistsQueryService _queryService = queryService;
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<MovieResult>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Genre> genres = await _genreRepository.GetListAsync(cancellationToken);

        var genreNames = genres.Select(g => g.Name).ToList();

        IEnumerable<string> invalidGenres = request.Genres
            .Except(genreNames, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (invalidGenres.Any())
        {
            string invalidGenresString = string.Join(", ", invalidGenres);
            return Error.Validation("genres", $"Invalid genres: {invalidGenresString}");
        }

        IEnumerable<Genre> validGenres = genres
            .Where(g => request.Genres.Contains(g.Name))
            .ToList();

        var id = new MovieId(Guid.NewGuid());
        var title = Title.Create(request.Title);
        var year = Year.Create(request.Year);
        var director = Director.Create(request.Director);
        var duration = Duration.Create(request.Duration);
        var poster = request.Poster is null ? null : Url.Create(request.Poster);
        var rate = request.Rate is null ? null : Rate.Create(request.Rate.Value);

        bool movieExists = await _queryService
            .CheckAsync(title, year, director, id, cancellationToken);

        if (movieExists)
        {
            return Error.Conflict(
                "Movie.Exists", $"Movie already exists: {title.Value}; {year.Value}; {director.Value}");
        }

        var newMovie = Movie.Create(id, title, year, director, duration, poster, rate, validGenres);

        _movieRepository.Add(newMovie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MovieResult(
            id.Value,
            request.Title,
            request.Year,
            request.Director,
            request.Duration,
            request.Poster,
            request.Rate,
            request.Genres);
    }
}
