using Microsoft.EntityFrameworkCore;
using Movies.Domain.Movies;
using Movies.Infrastructure.Persistence.Configurations;

namespace Movies.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

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
