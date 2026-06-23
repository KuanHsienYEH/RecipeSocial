namespace RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Exceptions;

public class Recipe
{
    public Guid Id{get; private set;}
    public Guid AuthorId{get; private set;}
    public string Title {get; private set;}= string.Empty;
    public string Description {get; private set;}= string.Empty;
    public string? ImageUrl { get; private set; }
    public DateTime CreatedAtUtc {get; private set;}
    private readonly List<Like> _likes = new();
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Like> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public int LikeCount => _likes.Count;
    public int CommentCount => _comments.Count;

    private Recipe() { }

    public bool ToggleLike (Guid userId)
    {
        var existing = _likes.FirstOrDefault(l => l.UserId == userId);
        if (existing is null)
        {
            _likes.Add(new Like(userId, Id));
            return true;
        }
        else
        {
            _likes.Remove(existing);
            return false;
        }

    }
    public void EnsureCanBeDeletedBy (Guid userId)
    {
        if (userId != AuthorId)
            throw new DomainException("Only the author can delete this recipe.");
    }
    public Comment AddComment (string content, Guid authorId)
    {
        var comment = new Comment(Id, authorId, content);
        _comments.Add(comment);
        return comment;
    }
    public Recipe(
        Guid authorId,
        string title,
        string description,
        string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");

        Id = Guid.NewGuid();
        AuthorId = authorId;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        CreatedAtUtc = DateTime.UtcNow;
    }
}