using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;

namespace CookingMaster.ViewModels
{
    [QueryProperty(nameof(IngredientCategories), "IngredientCategories")]
    [QueryProperty(nameof(Ingredients), "Ingredients")]
    [QueryProperty(nameof(IsLoggedIn), "IsLoggedIn")]
    public partial class HomeViewModel : BaseViewModel, IQueryAttributable
    {
        private IConnectivity _connectivity;
        private ICulinaryRecipeService _recipeService;
        private bool _isNotFirstAppearing;
        public HomeViewModel
            (
            IConnectivity connectivity,
            ICulinaryRecipeService recipeService       
            )
        {
            _connectivity = connectivity;
            _recipeService = recipeService;
        }

        #region Properties

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        private string searchRecipe;

        [ObservableProperty]
        private string searchIngredient;

        [ObservableProperty]
        private List<string> typeOfMeal;

        [ObservableProperty]
        private string selectedTypeOfMeal;

        [ObservableProperty]
        ObservableCollection<SelectedIngredient> selectedIngredients = new();

        [ObservableProperty]
        ObservableCollection<Ingredient> filteredIngredients;

        [ObservableProperty]
        ObservableCollection<CulinaryRecipeResponse> culinaryRecipes = new();

        [ObservableProperty]
        ObservableCollection<CulinaryRecipeResponse> filteredRecipes = new();


        #endregion


        #region Commands

        [RelayCommand]
        async Task AddNewRecipeAsync()
        {
            

            if (UserData.EmailAddress is null)
            { 
               await DisplayExceptionMessage.DisplayToast(ExceptionMessagesEnum.MustBeLoggedIn);
                return;
            }

            var culinaryRecipe = new CulinaryRecipeRequest
            {
                CuisineTypeUid = "",
                UserUid = UserData.UserUid,
                RecipeName = Title,
                Ingredients = SelectedIngredients.ToDictionary(item => item.IngredientUid, item => item.Amount),
                RecipeDescription = Description,
            };

          var result = await _recipeService.SaveRecipeAsync(culinaryRecipe);

            await DisplayExceptionMessage.DisplayAlert(result);
        }

        [RelayCommand]
        async Task LogoutAsync()
        {
            if (Preferences.ContainsKey(nameof(UserLogin)))
            {
                Preferences.Remove(nameof(UserLogin));
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
        }
        #endregion


        #region Methods
        internal async Task OnAppearingAsync()
        {
            
            CulinaryRecipes.Clear();
            UserName = UserData.UserName;
            if (_isNotFirstAppearing)
                await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering environment\n Please wait a few seconds..."));
            try
            {
                //var culinaryRecipes = SeedData.GetCulinaryRecipes();
                var culinaryRecipes = await _recipeService.GetRecipesAsync();

                if (culinaryRecipes is not null)
                {
                    foreach (var recipe in culinaryRecipes)
                    {
                        CulinaryRecipes.Add(recipe);
                    }
                }
                FilteredRecipes = new ObservableCollection<CulinaryRecipeResponse>(CulinaryRecipes);
                _isNotFirstAppearing = true;
                await MopupService.Instance.PopAllAsync();
            }
            catch (Exception)
            {
                await MopupService.Instance.PopAllAsync();
                throw;
            }

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Ingredients = query["Ingredients"] as ObservableCollection<Ingredient>;
            FilteredIngredients = new ObservableCollection<Ingredient>(Ingredients);
            TypeOfMeal = EnumHelper.GetEnumValuesAsStrings<TypeOfMeal>();
            SelectedTypeOfMeal = TypeOfMeal.First();
        }

        public void FilterRecipes()
        {
            FilteredRecipes.Clear();
            var _culinaryRecipes = CulinaryRecipes.Where(x => x.RecipeName.ToLower().Contains(SearchRecipe.ToLower())).ToList();

            foreach (var item in _culinaryRecipes)
            {
                FilteredRecipes.Add(item);
            }
        }

        public void FilterIngredients()
        {
            FilteredIngredients.Clear();
            var _ingredientsBySelectedTab = Ingredients.Where(x => x.IngredientName.ToLower().Contains(SearchIngredient.ToLower())).ToList();

            foreach (var item in _ingredientsBySelectedTab)
            {
                FilteredIngredients.Add(item);
            }
        }
        #endregion

    }
}
