using System;

namespace MasteringOOP.Inheritance;

// 'abstract' means we can never write: new AuditableEntity()
// It exists ONLY to be inherited by other classes.
public abstract class AuditableEntity
{
    // Properties are encapsulated (private set)
    public Guid Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public string CreatedBy { get; private set; }

    // 'protected' constructor means only this class and its children can call it.
    protected AuditableEntity(string createdBy)
    {
        Id = Guid.NewGuid(); // Generates a unique DB identifier
        CreatedAt = DateTimeOffset.UtcNow; // Standard for production APIs
        CreatedBy = createdBy;
    }
}