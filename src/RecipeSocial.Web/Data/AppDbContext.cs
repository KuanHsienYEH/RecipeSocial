using Microsoft.EntityFrameworkCore;
using RecipeSocial.Domain.Entities;

namespace RecipeSocial.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Like> Likes => Set<Like>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Like 的PK是 RecipeId + UserId 的組合
        modelBuilder.Entity<Like>()
            .HasKey(l => new { l.RecipeId, l.UserId });
    }
}