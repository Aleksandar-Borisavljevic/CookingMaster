namespace CookingMaster.Views.Pages;



#if ANDROID
using BottomSheetView =  Google.Android.Material.BottomSheet.BottomSheetDialog;
using BottomSheet;
#elif IOS || MACCATALYST
using BottomSheetView = UIKit.UIViewController;
#elif TIZEN
using BottomSheetView = Tizen.UIExtensions.NUI.Popup;
#else
using BottomSheetView = Microsoft.UI.Xaml.Controls.Primitives.Popup;
#endif

public partial class HomePage : ContentPage, IDisposable
{
    BottomSheetView? bottomSheet;
    BottomSheetView? bottomSheet2;
    HomeViewModel _homeViewModel;
    Ingredient _ingredient;
    
	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;
		BindingContext = homeViewModel;
        
    }

    #region Properties
    private string _selectedRadioAmount;
    public string SelectedRadioAmount
    {
        get => _selectedRadioAmount;
        set
        {
            if (_selectedRadioAmount != value)
            {
                _selectedRadioAmount = value;
                OnPropertyChanged(nameof(SelectedRadioAmount));
            }
        }
    }

    private short _amount;
    public short Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
    }
    #endregion

    protected async override void OnAppearing()
    {
       await _homeViewModel.OnAppearingAsync();
        
    }

    #region Events
    private void ReadRecipeBtn_Clicked(object sender, EventArgs e)
    {
        var culinaryRecipe = ((Button)sender).CommandParameter as CulinaryRecipeResponse;
        var view = (View)BottomSheetReadRecipe.CreateContent();
        view.BindingContext = culinaryRecipe;
        bottomSheet = this.ShowBottomSheet(view, true);

    }

    private void TapIngredient_Tapped(object sender, TappedEventArgs e)
    {
        _ingredient = e.Parameter as Ingredient;
        SelectedRadioAmount = "Quantity";
        var view = (View)BottomSheetChooseQuantity.CreateContent();
        view.BindingContext = this;
        bottomSheet2 = this.ShowBottomSheet(view, true);
    }

    private void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bottomSheet.CloseBottomSheet();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        _homeViewModel.FilterRecipes();
    }

    private void TxtSearchIngredient_TextChanged(object sender, TextChangedEventArgs e)
    {
        _homeViewModel.FilterIngredients();
    }

    private void RadioAmount_SelectionChanged(object sender, CheckedChangedEventArgs e)
    {
        var selectedRadioButton = sender as RadioButton;
        if (selectedRadioButton.IsChecked)
        {
            SelectedRadioAmount = selectedRadioButton.Content.ToString();
        }
    }

    private void BtnAddIngredient_Clicked(object sender, EventArgs e)
    {
        string ingredient = SelectedRadioAmount + ":" + Environment.NewLine + "(" + Amount +")";
        _homeViewModel.SelectedIngredients.Add(new SelectedIngredient { IngredientUid = _ingredient.Uid, Title = _ingredient.IngredientName, IconPath = _ingredient.IconPath, Amount = Amount });
        bottomSheet2.CloseBottomSheet();
    }

    private void StackFilter_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void StackNewRecipe_Tapped(object sender, TappedEventArgs e)
    {
        var view = (View)BottomSheetNewRecipe.CreateContent();
        view.BindingContext = _homeViewModel;
        bottomSheet = this.ShowBottomSheet(view, true);
        bottomSheet.Behavior.Draggable = false;
    }

    #endregion

    public void Dispose()
    {
#if !WINDOWS
        bottomSheet?.Dispose();
#endif
    }

    

    

    private void BtnClose_Clicked(object sender, EventArgs e)
    {
        bottomSheet2.CloseBottomSheet();
    }    
}

public struct SelectedIngredient
{
    public string IngredientUid { get; set; }
    public string Title { get; set; }

    public string IconPath { get; set; }

    public short Amount { get; set; }
}