using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Genres;
using Movies.Domain.Movies;

namespace Movies.Infrastructure.Persistence.Configurations;

internal sealed class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movie");

        builder.HasIndex(e => e.Id).IsUnique();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                v => v.Value, // from entity property to database
                v => new MovieId(v) // from database to entity property
            );

        builder.Property(e => e.Title)
            .HasConversion(
                v => v.Value,
                v => Title.Create(v))
            .HasMaxLength(Title.MaxLength)
            .IsRequired();

        builder.Property(e => e.Year)
            .HasConversion(
                v => v.Value,
                v => Year.Create(v));

        builder.Property(e => e.Director)
            .HasConversion(
                v => v.Value,
                v => Director.Create(v))
            .HasMaxLength(Director.MaxLength)
            .IsRequired();

        builder.Property(e => e.Duration)
            .HasConversion(
                v => v.Value,
                v => Duration.Create(v));

        builder.Property(e => e.Poster)
            .HasConversion(
                v => (v == null ? null : v.Value),
                v => v == null ? null : Url.Create(v))
            .HasColumnType("TEXT");

        builder.Property(e => e.Rate)
            .HasConversion<decimal?>(
                v => (v == null ? null : v.Value),
                v => v.HasValue ? Rate.Create(v.Value) : null)
            .HasPrecision(2, 1);

        builder.HasIndex(e => new { e.Title, e.Year, e.Director })
            .HasDatabaseName(
                $"AK_Movie_{nameof(Movie.Title)}_{nameof(Movie.Year)}_{nameof(Movie.Director)}")
            .IsUnique();

        builder.HasMany(e => e.Genres)
            .WithMany(e => e.Movies)
            .UsingEntity(
                "MovieGenre",
                l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
                r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId"));
    }
}
