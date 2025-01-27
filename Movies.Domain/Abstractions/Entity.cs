
namespace Movies.Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity
{
    private readonly bool _auditable;

    protected Entity(bool isAuditable = false)
    {
        _auditable = isAuditable;
    }

    public TEntityId Id { get; protected set; } = default!;

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public void SetCreatedAt(DateTime dateTime)
    {
        if (_auditable)
        {
            CreatedAt = dateTime;
        }
    }

    public void SetUpdatedAt(DateTime dateTime)
    {
        if (!_auditable)
        {
            UpdatedAt = dateTime;
        }
    }
}
