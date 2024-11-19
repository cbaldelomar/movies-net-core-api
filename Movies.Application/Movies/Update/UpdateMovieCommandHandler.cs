using Movies.Application.Abstractions;
using Movies.Application.Movies.Create;
using Movies.Domain.Abstractions;
using Movies.Domain.Genres;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.Update;

public sealed class UpdateMovieCommandHandler(
    IGenreRepository genreRepository,
    IMovieRepository movieRepository,
    ICheckMovieExistsQueryService queryService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMovieCommand, bool>
{
    private readonly IGenreRepository _genreRepository = genreRepository;
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly ICheckMovieExistsQueryService _queryService = queryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Genre> genres;

        var id = new MovieId(request.Id);

        Movie? movie = await _movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
        {
            return false;
        }


        if (request.Genres is null)
        {
            genres = movie.Genres;
        }
        else
        {
            IEnumerable<Genre> genreCatalog = await _genreRepository.GetListAsync(cancellationToken);
            var genreNames = genreCatalog.Select(g => g.Name).ToList();

            IEnumerable<string> invalidGenres = request.Genres
                .Except(genreNames, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (invalidGenres.Any())
            {
                return false;
            }

            genres = genreCatalog
                .Where(g => request.Genres.Contains(g.Name))
                .ToList();
        }

        var title = request.Title is null ? movie.Title : Title.Create(request.Title);
        var year = request.Year is null ? movie.Year : Year.Create(request.Year.Value);
        var director = request.Director is null ? movie.Director : Director.Create(request.Director);
        var duration = request.Duration is null ? movie.Duration : Duration.Create(request.Duration.Value);
        var poster = request.Poster is null ? movie.Poster : Url.Create(request.Poster);
        var rate = request.Rate is null ? movie.Rate : Rate.Create(request.Rate.Value);

        bool movieExists = await _queryService
            .CheckAsync(title, year, director, movie.Id, cancellationToken);

        if (movieExists)
        {
            return false;
        }

        movie.Update(title, year, director, duration, poster, rate, genres);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
