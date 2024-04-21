
namespace CookingMaster.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task SaveIngredientAsync(Tuple<string, string> Uids);
        Task RemoveIngredientAsync(Tuple<int, int> Ids);
    }
}
