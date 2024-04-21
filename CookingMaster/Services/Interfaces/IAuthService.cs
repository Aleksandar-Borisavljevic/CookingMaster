
namespace CookingMaster.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserKitchen> LoginAsync(UserLogin userLogin);

        Task<string> RegisterAsync(UserRegister userRegister);

        Task VerifyAsync(string verifyCode);

        Task<string> ForgotPasswordAsync(string email);

        Task ResetPasswordAsync(ResetPassword resetPassword);
    }
}
