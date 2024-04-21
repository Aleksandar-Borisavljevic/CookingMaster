
namespace CookingMaster.Popups;

public partial class IngredientDescriptionPopup
{
    public IngredientDescriptionPopup(Ingredient ingredient)
    {
        InitializeComponent();
        BindingContext = ingredient;
    }



    private async void BtnClose_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}