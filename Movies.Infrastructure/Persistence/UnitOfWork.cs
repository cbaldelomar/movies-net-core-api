using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movies.Domain.Abstractions;

namespace Movies.Infrastructure.Persistence;

internal sealed class UnitOfWork(
    ApplicationDbContext _context,
    IPublisher _publisher,
    IDateTimeProvider _dateTimeProvider) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        // Update auditable entities.
        UpdateAuditableEntities();

        // Publish domain events.
        await PublishDomainEvents();

        // Add more logic before save changes...

        await _context.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries.ToList())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(e => e.CreatedAt).CurrentValue = _dateTimeProvider.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.UpdatedAt).CurrentValue = _dateTimeProvider.Now;
            }
        }
    }

    private async Task PublishDomainEvents()
    {
        var domainEvents = _context.ChangeTracker.Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .SelectMany(e =>
            {
                // Make a copy of domain events list.
                var domainEvents = e.DomainEvents.ToList();

                // Clear domain events after copying them.
                e.ClearDomainEvents();

                // Return the copy of domain events list.
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
