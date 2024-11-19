using Movies.Application.Abstractions;
using Movies.Domain.Abstractions;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.Commands.Delete;

public sealed class DeleteMovieCommandHandler(
    IMovieRepository movieRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMovieCommand, bool>
{
    private readonly IMovieRepository _movieRepository = movieRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var id = new MovieId(request.Id);

        Movie? movie = await _movieRepository.GetByIdAsync(id, cancellationToken);

        if (movie is null)
        {
            return false;
        }

        _movieRepository.Remove(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
