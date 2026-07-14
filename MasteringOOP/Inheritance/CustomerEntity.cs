using System;

namespace MasteringOOP.Inheritance;

// The ':' means CustomerEntity IS AN AuditableEntity.
// It gets Id, CreatedAt, and CreatedBy for free!
public class CustomerEntity : AuditableEntity
{
    public string Email { get; private set; }
    public string FullName { get; private set; }

    // Notice we ask for 'createdBy' in this constructor, 
    // but we immediately hand it off to the base class using : base()
    public CustomerEntity(string fullName, string email, string createdBy) 
        : base(createdBy) 
    {
        FullName = fullName;
        Email = email;
    }

    public override string ToString()
    {
        // We can access Id and CreatedAt here because they were inherited!
        return $"[{Id}] {FullName} ({Email}) - Created By: {CreatedBy} at {CreatedAt:yyyy-MM-dd HH:mm}";
    }
}