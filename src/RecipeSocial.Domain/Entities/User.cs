namespace RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Exceptions;

public class User
{
    public Guid Id { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;

    private User() { }

    public User(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new DomainException("Display name cannot be empty.");

        if (displayName.Length > 50)
            throw new DomainException("Display name cannot exceed 50 characters.");

        Id = Guid.NewGuid();
        DisplayName = displayName.Trim();
    }
}