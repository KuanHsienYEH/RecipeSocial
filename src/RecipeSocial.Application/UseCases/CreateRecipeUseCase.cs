using RecipeSocial.Domain.Entities;
using RecipeSocial.Domain.Interfaces;

namespace RecipeSocial.Application.UseCases;

public class CreateRecipeUseCase
{
    private readonly IRecipeRepository _repository;

    public CreateRecipeUseCase(IRecipeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid authorId, string title, string description)
    {
        var recipe = new Recipe(authorId, title, description);
        await _repository.AddAsync(recipe);
        await _repository.SaveChangesAsync();
    }
}
