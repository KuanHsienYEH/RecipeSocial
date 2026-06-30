using Microsoft.EntityFrameworkCore;
using RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Interfaces;

namespace RecipeSocial.Infrastructure.Persistence;

public class RecipeRepository : IRecipeRepository
{
    private readonly AppDbContext _db;

    public RecipeRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Recipe>> GetAllAsync()
        => await _db.Recipes
            .Include(r => r.Likes)
            .Include(r => r.Comments)
            .OrderByDescending(r => r.CreatedAtUtc)
            .ToListAsync();

    public async Task<Recipe?> GetByIdAsync(Guid id)
        => await _db.Recipes
            .Include(r => r.Likes)
            .Include(r => r.Comments)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task AddAsync(Recipe recipe)
        => await _db.Recipes.AddAsync(recipe);

    public async Task SaveChangesAsync()
        => await _db.SaveChangesAsync();
}
