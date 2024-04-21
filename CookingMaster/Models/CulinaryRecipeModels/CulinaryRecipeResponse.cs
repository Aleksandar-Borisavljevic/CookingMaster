
namespace CookingMaster.Models.CulinaryRecipeModels
{
    public class CulinaryRecipeResponse : BaseCulinaryRecipe
    {
        public string Uid { get; set; }

        public string CuisineType { get; set; }
        public UserKitchen User { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
