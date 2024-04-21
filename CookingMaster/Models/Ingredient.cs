
namespace CookingMaster.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IconPath { get; set; }
        public short Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public IngredientNutrient IngredientNutrient { get; set; }
        public IngredientCategory IngredientCategory { get; set; }
        public string Uid { get; set; }

    }
}
