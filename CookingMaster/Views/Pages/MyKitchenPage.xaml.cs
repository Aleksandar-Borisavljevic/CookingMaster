
using CommunityToolkit.Maui.Core.Platform;
using Mopups.Services;

namespace CookingMaster.Views.Pages;

public partial class MyKitchenPage : ContentPage
{
    private MyKitchenViewModel _myKitchenViewModel;

    private CancellationTokenSource tokenSource = new CancellationTokenSource();



    public MyKitchenPage(MyKitchenViewModel myKitchenModel)
    {
        InitializeComponent();
        _myKitchenViewModel = myKitchenModel;
        BindingContext = myKitchenModel;
    }

    //protected async override void OnAppearing()
    //{
    //    await _myKitchenViewModel.LoadDataCommand.ExecuteAsync(null);

    //    _myKitchenViewModel.TabChangedCommand.Execute(_myKitchenViewModel.SelectedTab);

    //    await MopupService.Instance.PopAsync();
    //}

    private void ListenCancel()
    {
        tokenSource?.Cancel();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        _myKitchenViewModel.FilterIngredients();
    }

    private async void TapIngredient_Tapped(object sender, TappedEventArgs e)
    {
        //if (KeyboardExtensions.IsSoftKeyboardShowing(TxtSearchIngredient))
        //{
        //    await KeyboardExtensions.HideKeyboardAsync(TxtSearchIngredient, default);
        //}
        Ingredient ingredient = e.Parameter as Ingredient;
        await _myKitchenViewModel.SelectedIngredientAsync(ingredient);
    }

    private void GetRecipe_Clicked(object sender, EventArgs e)
    {
        var selectedIngredients = CollViewMyKitchen.SelectedItems.Select(x => (Ingredient)x).ToList();

        _myKitchenViewModel.GetRecipeCommand.ExecuteAsync(selectedIngredients);
    }

    protected async override void OnAppearing()
    {
       await _myKitchenViewModel.OnAppearingAsync();
        
    }


}

