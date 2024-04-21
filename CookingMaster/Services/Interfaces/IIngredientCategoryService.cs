
namespace CookingMaster.Services.Interfaces
{
   public interface IIngredientCategoryService
    {
        Task<IEnumerable<IngredientCategory>> GetIngredientCategoriesAsync();
    }
}
