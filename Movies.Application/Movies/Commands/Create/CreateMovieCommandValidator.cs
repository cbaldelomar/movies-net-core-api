using FluentValidation;
using Movies.Domain.Movies;

namespace Movies.Application.Movies.Commands.Create;

internal sealed class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(Title.MaxLength)
            .WithMessage($"Title must not exceed {Title.MaxLength} characters.");

        RuleFor(x => x.Year)
            .InclusiveBetween(Year.MinYear, Year.MaxYear)
            .WithMessage($"Year must be between {Year.MinYear} and {Year.MaxYear}.");

        RuleFor(x => x.Director)
            .NotEmpty()
            .WithMessage("Director is required.")
            .MaximumLength(Director.MaxLength)
            .WithMessage($"Director must not exceed {Director.MaxLength} characters.");

        RuleFor(x => x.Duration)
            .InclusiveBetween(Duration.MinDuration, Duration.MaxDuration)
            .WithMessage($"Duration must be between {Duration.MinDuration} and {Duration.MaxDuration} minutes.");

        RuleFor(x => x.Poster)
            .Must(BeValidUrl)
            .WithMessage("Poster must be a valid URL.")
            .When(x => x.Poster is not null);

        RuleFor(x => x.Rate)
            .InclusiveBetween(Rate.MinRate, Rate.MaxRate)
            .WithMessage($"Rate must be between {Rate.MinRate} and {Rate.MaxRate}.")
            .When(x => x.Rate is not null);
    }

    private static bool BeValidUrl(string? poster)
    {
        return Url.IsValidUrl(poster);
    }
}
