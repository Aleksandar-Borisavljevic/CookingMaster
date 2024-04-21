using System.Collections.ObjectModel;

namespace CookingMaster.Models.CulinaryRecipeModels
{
    public class CulinaryRecipesByDishes
    {
        public string TypeOfDish { get; set; }

        public ObservableCollection<CulinaryRecipeResponse> CulinaryRecipes { get; set; } = new();
    }
}
