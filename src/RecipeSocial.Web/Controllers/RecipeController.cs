using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeSocial.Web.Data;
using RecipeSocial.Domain.Entities;


namespace RecipeSocial.Web.Controllers;

public class RecipeController : Controller
{
    private readonly AppDbContext _db;

    public RecipeController(AppDbContext db)
    {
        _db = db;
    }

    // GET /Recipe
    public async Task<IActionResult> Index()
    {
        var recipes = await _db.Recipes
            .Include(r => r.Likes)
            .Include(r => r.Comments)
            .OrderByDescending(r => r.CreatedAtUtc)
            .ToListAsync();

        return View(recipes);
    }

    // GET /Recipe/Detail/{id}
    public async Task<IActionResult> Detail(Guid id)
    {
        var recipe = await _db.Recipes
            .Include(r => r.Likes)
            .Include(r => r.Comments)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe is null) return NotFound();

        return View(recipe);
    }
    // POST /Recipe/ToggleLike/{id}
    [HttpPost]
    public async Task<IActionResult> ToggleLike(Guid id)
    {
        var recipe = await _db.Recipes
            .Include(r => r.Likes)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe is null) return NotFound();

        // 暫時先用固定的 userId 模擬「目前登入的使用者」
        var currentUserId = await _db.Users
            .Select(u => u.Id)
            .FirstAsync();

        recipe.ToggleLike(currentUserId);
        await _db.SaveChangesAsync();

        return RedirectToAction("Detail", new { id });
    }
    // POST /Recipe/AddComment/{id}
    [HttpPost]
    public async Task<IActionResult> AddComment(Guid id, string content)
    {
        var recipe = await _db.Recipes
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe is null) return NotFound();

        var currentUserId = await _db.Users
            .Select(u => u.Id)
            .FirstAsync();

        var comment = recipe.AddComment(content, currentUserId);
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return RedirectToAction("Detail", new { id });
    }
    public IActionResult Create()
    {
        return View();
    }

    // POST /Recipe/Create
    [HttpPost]
    public async Task<IActionResult> Create(string title, string description)
    {
        var currentUserId = await _db.Users
            .Select(u => u.Id)
            .FirstAsync();

        var recipe = new Recipe(currentUserId, title, description);
        _db.Recipes.Add(recipe);
        await _db.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}