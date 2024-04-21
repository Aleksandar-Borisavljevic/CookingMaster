namespace CookingMaster.Views.Pages;

public partial class MyCookBookPage : ContentPage
{
    MyCookBookViewModel _myCookBookViewModel;
    public MyCookBookPage(MyCookBookViewModel myCookBookViewModel)
    {
        InitializeComponent();
        _myCookBookViewModel = myCookBookViewModel;
        BindingContext = _myCookBookViewModel;
    }

    protected async override void OnAppearing()
    {
        await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering recipes\n Please wait a few seconds..."));
        await _myCookBookViewModel.OnAppearingAsync();
        await MopupService.Instance.PopAllAsync();
    }
}