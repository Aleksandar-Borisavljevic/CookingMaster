
namespace CookingMaster.Services.Interfaces
{
    public interface ICulinaryRecipeService
    {
        Task<string> SaveRecipeAsync(CulinaryRecipeRequest culinaryRecipe);

        Task<IEnumerable<CulinaryRecipeResponse>> GetRecipesAsync(int UserId = 0);
    }
}
