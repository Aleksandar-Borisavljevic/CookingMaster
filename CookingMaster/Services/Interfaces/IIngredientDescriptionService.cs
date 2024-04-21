
namespace CookingMaster.Services.Interfaces
{
   public interface IIngredientDescriptionService
    {
        Task<IngredientDescription> GetIngredientDescriptionAsync(int ingredientId);
    }
}
