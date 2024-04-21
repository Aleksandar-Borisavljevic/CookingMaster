
using Newtonsoft.Json;

namespace CookingMaster.Models
{
    public class UserKitchen
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        [JsonProperty("Ingredients")]
        public IEnumerable<Ingredient> Ingredients { get; set; }

        public string UserUid { get; set; }
    }
}



