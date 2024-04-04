using Microsoft.EntityFrameworkCore;
using Movies.Domain.Movies;

namespace Movies.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar todas las configuraciones que se encuentren en el assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
