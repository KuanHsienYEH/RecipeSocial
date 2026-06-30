using RecipeSocial.Domain.Entities;
using RecipeSocial.Infrastructure.Persistence; 

namespace RecipeSocial.Web.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Recipes.Any()) return;

        var alice = new User("Alice");
        var bob = new User("Bob");
        var carol = new User("Carol");

        db.Users.AddRange(alice, bob, carol);

        var recipe1 = new Recipe(alice.Id, "Spaghetti Aglio e Olio", "Simple garlic and olive oil pasta ready in 20 minutes.");
        var recipe2 = new Recipe(bob.Id, "Avocado Toast", "Creamy avocado on sourdough with a pinch of chili flakes.");
        var recipe3 = new Recipe(carol.Id, "Miso Soup", "Classic Japanese miso soup with tofu and wakame.");

        recipe1.ToggleLike(bob.Id);
        recipe1.ToggleLike(carol.Id);
        recipe2.ToggleLike(alice.Id);
        recipe1.AddComment("Looks delicious!", bob.Id);
        recipe2.AddComment("I make this every morning!", carol.Id);

        db.Recipes.AddRange(recipe1, recipe2, recipe3);
        db.SaveChanges();
    }
}