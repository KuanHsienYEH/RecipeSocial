using RecipeSocial.Domain.Entities;

namespace RecipeSocial.Domain.Interfaces;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(Guid id);
    Task AddAsync(Recipe recipe);
    Task SaveChangesAsync();
}