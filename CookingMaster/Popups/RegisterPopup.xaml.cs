
namespace CookingMaster.Popups;

public partial class RegisterPopup
{
    public RegisterPopup(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        BindingContext = registerViewModel;
    }
}