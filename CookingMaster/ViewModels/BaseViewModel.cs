using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CookingMaster.ViewModels
{

    public partial class BaseViewModel : ObservableObject
    {
      
        
        public BaseViewModel()
        {

        }

        #region Properties

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        public static string bgImage;

        [ObservableProperty]
        public static string language = "English";

        [ObservableProperty]
        public static UserKitchen userData;

        [ObservableProperty]
        static ObservableCollection<Ingredient> ingredients;

        [ObservableProperty]
        static ObservableCollection<IngredientCategory> ingredientCategories;

        [ObservableProperty]
        static ObservableCollection<CuisineType> cuisineTypes = new();

        [ObservableProperty]
        bool isLoggedIn;

        //public string UserNamee => userData.UserName;


        #endregion
    }
}
