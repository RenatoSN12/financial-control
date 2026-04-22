namespace FinanceControl.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected Entity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
    }
}