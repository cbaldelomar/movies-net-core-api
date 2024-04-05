using Movies.Application.Abstractions;
using Movies.Domain.Abstractions;
using Movies.Domain.Genres;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.Create;

public sealed class CreateMovieCommandHandler(
    IGenreRepository genreRepository,
    IMovieRepository movieRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovieCommand, MovieResult?>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<MovieResult?> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Genre> genres = await _genreRepository.GetListAsync(cancellationToken);

        var genreNames = genres.Select(g => g.Name).ToList();

        IEnumerable<string> invalidGenres = request.Genres
            .Except(genreNames, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (invalidGenres.Any())
        {
            return null;
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
