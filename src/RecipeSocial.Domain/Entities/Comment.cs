namespace RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Exceptions;

public class Comment
{
    public Guid Id { get; private set; }
    public Guid RecipeId { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAtUtc { get; private set; }
    
    // 給 EF Core 用，外部不能呼叫
    private Comment() { }

    // internal：只有 Domain 專案內部可以呼叫
    // 所以外部只能透過 Recipe.AddComment() 進來，不能直接 new Comment()
    internal Comment(Guid recipeId, Guid authorId, string content)
    {
       if (recipeId == Guid.Empty)
            throw new DomainException("RecipeId is required.");

        if (authorId == Guid.Empty)
            throw new DomainException("AuthorId is required.");

        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Comment content cannot be empty.");

        if (content.Length > 500)
            throw new DomainException("Comment cannot exceed 500 characters.");

        Id = Guid.NewGuid();
        RecipeId = recipeId;
        AuthorId = authorId;
        Content = content.Trim();
        CreatedAtUtc = DateTime.UtcNow;

    }

}