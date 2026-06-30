namespace RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Exceptions;

public class Like
{
    public Guid RecipeId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAtUtc {get; private set;}

    private Like() { }

    internal Like(Guid userId, Guid recipeId)
    {
        if (recipeId == Guid.Empty)
            throw new DomainException("RecipeId is required.");

        if (userId == Guid.Empty)
            throw new DomainException("UserId is required.");

        RecipeId = recipeId;
        UserId = userId;
        CreatedAtUtc = DateTime.UtcNow;
    }

}