
namespace CookingMaster.Models
{
    public class ResetPassword
    {
        public string Token { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
