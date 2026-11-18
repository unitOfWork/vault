namespace Vault.Services.OnBoarding.Domain.Common;

/// <summary>
/// Base class for entities, identified by a time-ordered (version 7) Guid assigned on creation.
/// </summary>
public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.CreateVersion7();
    }

    public Guid Id { get; private set; }
}
