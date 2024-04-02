using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Genres;

namespace Movies.Infrastructure.Persistence.Configurations;

internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");

        builder.HasIndex(e => e.Id).IsUnique();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(
                v => v.Value, // from entity property to database
                v => new GenreId(v) // from database to entity property
            );

        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"AK_{nameof(Genre)}_{nameof(Genre.Name)}")
            .IsUnique();
    }
}
