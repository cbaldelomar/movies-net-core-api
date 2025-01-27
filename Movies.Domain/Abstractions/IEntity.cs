namespace Movies.Domain.Abstractions;

public interface IEntity
{
    DateTime CreatedAt { get; }

    DateTime? UpdatedAt { get; }

    void SetCreatedAt(DateTime dateTime);

    void SetUpdatedAt(DateTime dateTime);
}
