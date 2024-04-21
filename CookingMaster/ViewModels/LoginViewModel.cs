using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CookingMaster.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        IAuthService _authService;
        IIngredientCategoryService _ingredientCategoryService;
        ICuisineTypeService _cuisineTypeService;
        IIngredientService _ingredientService;
        RegisterViewModel _registerViewModel;
        ForgotPasswordViewModel _forgotPasswordViewModel;

        public LoginViewModel(
            IAuthService authService,
            RegisterViewModel registerViewModel,
            ForgotPasswordViewModel forgotPasswordViewModel,
            IIngredientCategoryService ingredientCategoryService,
            IIngredientService ingredientService,
            ICuisineTypeService cuisineTypeService)
        {
            _authService = authService;
            _registerViewModel = registerViewModel;
            _forgotPasswordViewModel = forgotPasswordViewModel;
            _ingredientCategoryService = ingredientCategoryService;
            _ingredientService = ingredientService;
            _cuisineTypeService = cuisineTypeService;
            // AutoLoginCommand.ExecuteAsync(null);
        }


        #region Properties



        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isRememberMe = true;

        [ObservableProperty]
        private bool isLoggedIn;

        [ObservableProperty]
        private string userName;

        private ObservableCollection<Ingredient> Ingredients { get; } = new();

        private ObservableCollection<Ingredient> MyKitchen { get; } = new();

        private ObservableCollection<IngredientCategory> IngredientCategories { get; } = new();

        #endregion

        #region Commands
        [RelayCommand]
        async Task AutoLoginAsync()
        {
            try
            {
                await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering environment\n Please wait a few seconds..."));
                string userDetails = Preferences.Get(nameof(UserLogin), null);

                if (!string.IsNullOrWhiteSpace(userDetails))
                {
                    UserLogin userLogin = JsonConvert.DeserializeObject<UserLogin>(userDetails);

                    //var result = SeedData.LoginSeed(userLogin);
                    var result = await _authService.LoginAsync(userLogin);

                    if (result is null)
                    {
                        await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.DefaultError);
                        await MopupService.Instance.PopAsync();
                        return;
                    }

                    UserData = result;

                    //await LoadSeedData();
                    await LoadDataAsync();

                    await MopupService.Instance.PopAsync();
                }
                else
                    await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.DefaultError);
                await MopupService.Instance.PopAsync();
            }
        }

        [RelayCommand]
        async Task LoginAsync()
        {
            try
            {
                await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering environment\n Please wait a few seconds..."));
                if (!await ValidateUser(Email, Password))
                {
                    await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.InvalidEmailOrPass);
                    await MopupService.Instance.PopAsync();
                    return;
                }

                var userLogin = new UserLogin
                {
                    EmailAddress = Email,
                    Password = Password
                };

                //var userResponse = SeedData.LoginSeed(userLogin);
                var userResponse = await _authService.LoginAsync(userLogin);

                if (Preferences.ContainsKey(nameof(UserLogin)))
                {
                    Preferences.Remove(nameof(UserLogin));
                }

                if (userResponse is null)
                {
                    await MopupService.Instance.PopAsync();
                    await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.InvalidEmailOrPass);
                    return;
                }

                UserData = userResponse;

                string userDetails = JsonConvert.SerializeObject(userLogin);
                if (IsRememberMe)
                    Preferences.Set(nameof(UserLogin), userDetails);

                //await LoadSeedData();
                await LoadDataAsync();
            }

            catch (ArgumentException)
            {
                await MopupService.Instance.PopAsync();
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.InvalidEmailOrPass);
            }
            catch (Exception)
            {
                await MopupService.Instance.PopAsync();
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.DefaultError);
            }
        }
        [RelayCommand]
        async Task RegisterAsync()
        {
            await MopupService.Instance.PushAsync(new RegisterPopup(_registerViewModel));
        }

        [RelayCommand]
        async Task LoginAsGuestAsync()
        {
            try
            {
                await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering kitchen..."));

                UserData = new UserKitchen
                {
                    UserName = "Guest"
                };

                await LoadDataAsync();

                await MopupService.Instance.PopAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [RelayCommand]
        async Task ForgotPasswordAsync()
        {
            await MopupService.Instance.PushAsync(new ForgotPasswordPopup(_forgotPasswordViewModel));
        }

        #endregion

        #region Methods

        private async Task LoadSeedData()
        {
            ClearData();
            IEnumerable<IngredientCategory> ingredientCategories = null;
            IEnumerable<Ingredient> ingredients = null;

            //ingredientCategories = SeedData.GetIngredientCategories();
            //ingredients = SeedData.GetIngredients();

            foreach (var item in ingredientCategories)
            {
                IngredientCategories.Add(item);
            }

            foreach (var item in ingredients)
            {
                Ingredients.Add(item);
            }

            UserName = UserData.UserName;

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true,
                new Dictionary<string, object>
                            {
                        {"IngredientCategories", IngredientCategories},
                        {"Ingredients", Ingredients},
                        {"MyKitchen", MyKitchen},
                        {"IsLoggedIn", IsLoggedIn},
                        {"UserName", UserName}
                            });
        }
        private async Task LoadDataAsync()
        {
            ClearData();
            IEnumerable<IngredientCategory> ingredientCategories = null;
            IEnumerable<Ingredient> ingredients = null;
            IEnumerable<CuisineType> cuisineTypes = null;

            //TODO: Refactor this code
            IsLoggedIn = UserData.EmailAddress is null ? true : false;

            try
            {
                var task1 = await Task.Factory.StartNew(async () =>
                {
                    ingredientCategories = await _ingredientCategoryService.GetIngredientCategoriesAsync();
                });

                var task2 = await Task.Factory.StartNew(async () =>
                {
                    ingredients = await _ingredientService.GetIngredientsAsync();
                });

                var task3 = await Task.Factory.StartNew(async () =>
                {
                    cuisineTypes = await _cuisineTypeService.GetCuisineTypesAsync();
                });

                await task1;

                await task2;

                await task3;

                foreach (var item in ingredientCategories)
                {
                    IngredientCategories.Add(item);
                }

                foreach (var item in ingredients)
                {
                    Ingredients.Add(item);
                }

                foreach (var item in cuisineTypes)
                {
                    CuisineTypes.Add(item);
                }

                UserName = UserData.UserName;

                await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true,
                    new Dictionary<string, object>
                                {
                        {"IngredientCategories", IngredientCategories},
                        {"Ingredients", Ingredients},
                        {"IsLoggedIn", IsLoggedIn},
                        {"UserName", UserName}
                                });

                //AllIngredients = GetIngredientsWithoutKitchen(ingredients, UserData.Ingredients ?? new List<Ingredient>());

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private async Task<bool> ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.EmptyEmail);
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.EmptyPassword);
                return false;
            }
            return true;
        }

        private void ClearData()
        {
            Ingredients.Clear();
            IngredientCategories.Clear();
            MyKitchen.Clear();
        }
        #endregion


    }
}
