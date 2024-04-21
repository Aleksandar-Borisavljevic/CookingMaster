using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace CookingMaster.Helpers
{
    public class DisplayExceptionMessage
    {
        public static async Task DisplayAlert(string Messages)
        {

            await Shell.Current.DisplayAlert("Error!", $"{Messages}", "Ok");
        }

        public static async Task DisplaySnack(ExceptionMessagesEnum exceptionMessages)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackBarOption = new SnackbarOptions
            {

            };

            var snackBar = Snackbar.Make($"{exceptionMessages.GetEnumDescription()}");
            await snackBar.Show(cancellationTokenSource.Token);
        }

        public static async Task DisplayToast(ExceptionMessagesEnum exceptionMessages)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackBar = Toast.Make($"{exceptionMessages.GetEnumDescription()}");
            await snackBar.Show(cancellationTokenSource.Token);
        }

        public static async Task DisplaySnack(string Messages)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackBarOption = new SnackbarOptions
            {
                
            };

            var snackBar = Snackbar.Make(Messages);
            await snackBar.Show(cancellationTokenSource.Token);
        }
    }
}
