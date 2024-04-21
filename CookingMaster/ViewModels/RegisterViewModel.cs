using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        IAuthService _authService;
        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }


        #region Properties

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string emailAddress;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmPassword;

        [ObservableProperty]
        private bool isVerifyVisible;

        [ObservableProperty]
        private string verifyCode;

        [ObservableProperty]
        private bool registerIsDisabled = true;

        #endregion

        #region Commands
        [RelayCommand]
        async Task RegisterAsync()
        {
            await MopupService.Instance.PushAsync(new SpinnerPopup("Creating account\n Please wait a few seconds..."));
            var userRegister = new UserRegister
            {
                Username = UserName,
                EmailAddress = EmailAddress,
                Password = Password,
                ConfirmPassword = ConfirmPassword
            };

            try
            {
                await _authService.RegisterAsync(userRegister);

                IsVerifyVisible = true;
                RegisterIsDisabled = false;
            }
            catch (Exception)
            {
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.DefaultError);
            }
            finally
            {
                await MopupService.Instance.PopAsync();
            }
        }

        [RelayCommand]
        async Task CancelAsync()
        {
            await MopupService.Instance.PopAsync(true);
        }

        [RelayCommand]
        async Task VerifyAsync()
        {
            try
            {
                await _authService.VerifyAsync(VerifyCode);

                await MopupService.Instance.PopAsync(true);
            }
            catch (Exception)
            {

                throw;
            }
        }


        
        #endregion

        #region Methods

        #endregion
    }
}
