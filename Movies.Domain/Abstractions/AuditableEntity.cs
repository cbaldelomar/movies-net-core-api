namespace Movies.Domain.Abstractions;

public abstract class AuditableEntity<TEntityId> : Entity<TEntityId>, IAuditableEntity
{
    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }
}
