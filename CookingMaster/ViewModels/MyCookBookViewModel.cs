using CommunityToolkit.Mvvm.ComponentModel;
using CookingMaster.Models.CulinaryRecipeModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.ViewModels
{
    public partial class MyCookBookViewModel : BaseViewModel
    {
        ICulinaryRecipeService _culinaryRecipeService;

        public MyCookBookViewModel(ICulinaryRecipeService culinaryRecipeService)
        {

            _culinaryRecipeService = culinaryRecipeService;
        }

        #region Properties
        public ObservableCollection<CulinaryRecipesByDishes> CulinaryRecipesByDishes { get; set; } = new();

        #endregion


        #region Commands

        [RelayCommand]
        async Task LogoutAsync()
        {
            if (Preferences.ContainsKey(nameof(UserLogin)))
            {
                Preferences.Remove(nameof(UserLogin));
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
        }

        [RelayCommand]
        internal async Task OnAppearingAsync()
        {

            var recipes = await _culinaryRecipeService.GetRecipesAsync(1);

            if (recipes is null)
                return;

            if (recipes.Count() == CulinaryRecipesByDishes.Count)
                return;

            CulinaryRecipesByDishes.Clear();

            var enumValues = EnumHelper.GetEnumValuesAsStrings<TypeOfMeal>();
            foreach (var enumValue in enumValues)
            {
                CulinaryRecipesByDishes culinaryRecipesByDishes = new CulinaryRecipesByDishes
                {
                    TypeOfDish = enumValue
                };

                var filteredRecipes = recipes;

                foreach (var recipe in filteredRecipes)
                {
                    culinaryRecipesByDishes.CulinaryRecipes.Add(recipe);
                }

                CulinaryRecipesByDishes.Add(culinaryRecipesByDishes);

            }
        }

        #endregion
    }
}
