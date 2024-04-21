
namespace CookingMaster.Models
{
    public class UserLogin
    {
        //TODO: Implement ability to login using Username not just user email
        //public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserUid { get; set; }
    }
}
