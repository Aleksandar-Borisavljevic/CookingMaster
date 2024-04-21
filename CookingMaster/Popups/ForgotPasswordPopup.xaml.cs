
namespace CookingMaster.Popups;

public partial class ForgotPasswordPopup
{
    public ForgotPasswordPopup(ForgotPasswordViewModel forgotPasswordViewModel)
    {
        InitializeComponent();
        BindingContext = forgotPasswordViewModel;
    }
}