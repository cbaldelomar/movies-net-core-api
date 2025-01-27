using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movies.Domain.Abstractions;

namespace Movies.Infrastructure.Persistence;

internal sealed class UnitOfWork(
    ApplicationDbContext context,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPublisher _publisher = publisher;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        // Update auditable entities.
        UpdateAuditableEntities();

        // Add more logic before save changes...

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // Publish domain events.
        await PublishDomainEventsAsync();
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IEntity>> entries =
            _context.ChangeTracker.Entries<IEntity>();

        foreach (var entry in entries.ToList())
        {
            IEntity entity = entry.Entity;
            DateTime dateTime = _dateTimeProvider.Now;

            if (entry.State == EntityState.Added)
            {
                entity.SetCreatedAt(dateTime);
            }

            if (entry.State == EntityState.Modified)
            {
                entity.SetUpdatedAt(dateTime);
            }
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        IDomainEvent[] domainEvents = _context.ChangeTracker.Entries<IAggregateRoot>()
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
            .ToArray();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
