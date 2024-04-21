namespace CookingMaster.Views.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
	{
        loginViewModel.AutoLoginCommand.ExecuteAsync(null);
        InitializeComponent();
        BindingContext = loginViewModel;
	}
}