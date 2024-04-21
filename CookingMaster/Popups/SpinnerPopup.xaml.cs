
namespace CookingMaster.Popups;

public partial class SpinnerPopup
{
    public SpinnerPopup(string message)
    {
        InitializeComponent();
        BindingContext = message;
    }


    #region Properties

    #endregion
}