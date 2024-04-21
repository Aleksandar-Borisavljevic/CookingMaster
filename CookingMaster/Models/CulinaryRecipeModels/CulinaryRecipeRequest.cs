using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.Models.CulinaryRecipeModels
{
    public class CulinaryRecipeRequest : BaseCulinaryRecipe
    {
        public string CuisineTypeUid { get; set; }
        public string UserUid { get; set; }
        public Dictionary<string, short> Ingredients { get; set; }
    }
}
