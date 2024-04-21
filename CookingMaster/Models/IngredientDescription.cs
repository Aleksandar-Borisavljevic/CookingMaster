
namespace CookingMaster.Models
{
    public class IngredientDescription
    {
        public int DescriptionId { get; set; }       
        public Ingredient Ingredient { get; set; }
        public string Description { get; set; }
    }
}
