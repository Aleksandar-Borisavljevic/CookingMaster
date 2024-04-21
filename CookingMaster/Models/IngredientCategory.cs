
namespace CookingMaster.Models
{
    public class IngredientCategory
    {
        //public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string IconPath { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public string Uid { get; set; } = string.Empty;
    }
}

