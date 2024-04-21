using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.ViewModels
{
    public partial class CulinaryRecipeViewModel : BaseViewModel
    {
        ICulinaryRecipeService _culinaryRecipeService;
        public CulinaryRecipeViewModel(ICulinaryRecipeService culinaryRecipeService)
        {
            _culinaryRecipeService = culinaryRecipeService;
        }

        #region Properties

        [ObservableProperty]
        public bool isSaveEnabled;

        [ObservableProperty]
        public string recipe;

        public string TypeOfDish { get; set; }

        [ObservableProperty]
        public bool isPrivate;

        #endregion

        #region Commands

        [RelayCommand]
        public async Task SaveAsync()
        {
            await MopupService.Instance.PushAsync(new SpinnerPopup("Saving recipe..."));
            if (UserData.EmailAddress is null)
            {
                await DisplayExceptionMessage.DisplaySnack("You cannot save a recipe as a guest.");
                await MopupService.Instance.PopAsync();
                return;
            }
            var culinaryRecipe = new CulinaryRecipeRequest
            {
                CuisineTypeUid = TypeOfDish,
                UserUid = UserData.UserUid,
                RecipeName = GetRecipeTitle(Recipe),
                Ingredients = new Dictionary<string, short>(),
                RecipeDescription = GetRecipeDescription(Recipe),
            };
            try
            {
                await _culinaryRecipeService.SaveRecipeAsync(culinaryRecipe);
                await MopupService.Instance.PopAllAsync();
                await DisplayExceptionMessage.DisplaySnack("Successfully saved!!!");
            }
            catch (Exception ex)
            {
                await MopupService.Instance.PopAsync();
                throw;
            }

        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            await MopupService.Instance.PopAsync();
        }

        #endregion

        #region Methods

        public string GetRecipeTitle(string recipe)
        {
            int startIndex = recipe.IndexOf("\n\n") + 2;
            int endIndex = recipe.IndexOf("\n\n", startIndex);
            return recipe.Substring(startIndex, endIndex - startIndex);
        }

        public string GetRecipeDescription(string recipe)
        {
            int startIndex = recipe.IndexOf("\n\n") + 2;
            int endIndex = recipe.IndexOf("\n\n", startIndex);
            return recipe.Substring(endIndex);
        }

        #endregion
    }
}
