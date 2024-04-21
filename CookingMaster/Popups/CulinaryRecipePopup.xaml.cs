
namespace CookingMaster.Popups;

public partial class CulinaryRecipePopup
{
    public CulinaryRecipePopup(CulinaryRecipeViewModel culinaryRecipeViewModel = null)
    {
        InitializeComponent();
        BindingContext = culinaryRecipeViewModel;
    }
}