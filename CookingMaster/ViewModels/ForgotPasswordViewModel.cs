using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.ViewModels
{
    public partial class ForgotPasswordViewModel : BaseViewModel
    {
        IAuthService _authService;
        public ForgotPasswordViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        #region Properties
        [ObservableProperty]
        private string token;

        [ObservableProperty]
        private string emailAddress;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private string isEnabled;

        #endregion

        #region Commands

        [RelayCommand]
        async Task ForgotPasswordAsync()
        {
            try
            {
                await MopupService.Instance.PushAsync(new SpinnerPopup("Loading..."));

                await _authService.ForgotPasswordAsync(EmailAddress);

            }
            catch (ArgumentException)
            {
                //TODO: Change ForgotPassword method to return string error message from API
                await DisplayExceptionMessage.DisplayAlert("Temporary msg");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await MopupService.Instance.PopAsync();
            }
        }

        [RelayCommand]
        async Task ResetPasswordAsync()
        {

            var userRegister = new ResetPassword
            {
                Token = Token,
                EmailAddress = EmailAddress,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };

            try
            {
                await _authService.ResetPasswordAsync(userRegister);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await MopupService.Instance.PopAsync(true);
        }
        #endregion
    }
}
