using System.Globalization;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Net;

namespace CookingMaster.ViewModels
{
    public partial class MyKitchenViewModel : BaseViewModel, IQueryAttributable
    {
        private IConnectivity _connectivity;
        private ISpeechToText _speechToText;
        private IIngredientService _ingredientService;
        private IIngredientDescriptionService _ingredientDescriptionService;
        private IOpenAIService _openAIService;
        private IAlertService _alertService;
        private CulinaryRecipeViewModel _culinaryRecipeViewModel;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        public MyKitchenViewModel
            (
            IConnectivity connectivity,
            ISpeechToText speechToText,
            IIngredientService ingredientService,
            IIngredientDescriptionService ingredientDescriptionService,
            IOpenAIService openAIService,
            IAlertService alertService,
            CulinaryRecipeViewModel culinaryRecipeViewModel
            )
        {
            _connectivity = connectivity;
            _speechToText = speechToText;
            _ingredientService = ingredientService;
            _ingredientDescriptionService = ingredientDescriptionService;
            _openAIService = openAIService;
            _alertService = alertService;
            _culinaryRecipeViewModel = culinaryRecipeViewModel;
        }

        #region Properties
        [ObservableProperty]
        string recognitionText;

        public bool IsLongPress { get; set; }

        [ObservableProperty]
        public bool isLoggedIn;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string searchIngredient;

        [ObservableProperty]
        ObservableCollection<Ingredient> ingredientsBySelectedTab = new();

        [ObservableProperty]
        ObservableCollection<Ingredient> ingredientsWithoutKitchen = new();

        [ObservableProperty]
        ObservableCollection<Ingredient> myKitchen = new();

        //public ObservableCollection<Ingredient> SelectedIngredients { get; set; } = new ObservableCollection<Ingredient>();

        #endregion        

        #region Tabs
        private ObservableCollection<TabViewModel> _tabs { get; set; } = new();
        private TabViewModel _selectedTab { get; set; }

        public ObservableCollection<TabViewModel> Tabs
        {
            get => _tabs;
            set { _tabs = value; OnPropertyChanged(nameof(Tabs)); }
        }

        public TabViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {

                _selectedTab = value;

                OnPropertyChanged(nameof(SelectedTab));

            }
        }

        internal TabViewModel GetTabs(IEnumerable<IngredientCategory> ingredientCategories)
        {
            Tabs.Clear();

            if (ingredientCategories != null)
            {
                foreach (var category in ingredientCategories)
                {
                    Tabs.Add(new TabViewModel { TabUid = category.Uid, TabTitle = category.CategoryName, IconPath = category.IconPath });
                }
                Tabs.First().IsSelected = true;
            }
            return Tabs.FirstOrDefault();
        }

        private ObservableCollection<TabViewModel> _tabsTypeOfDish { get; set; } = new();
        private TabViewModel _selectedTabTypeOfDish { get; set; }

        public ObservableCollection<TabViewModel> TabsTypeOfDish
        {
            get => _tabsTypeOfDish;
            set { _tabsTypeOfDish = value; OnPropertyChanged(nameof(TabsTypeOfDish)); }
        }

        public TabViewModel SelectedTabTypeOfDish
        {
            get => _selectedTabTypeOfDish;
            set
            {

                _selectedTabTypeOfDish = value;

                OnPropertyChanged(nameof(SelectedTabTypeOfDish));

            }
        }

        #endregion

        #region Commands

        [RelayCommand]
        async Task ListenAsync()
        {
            var isAuthorized = await _speechToText.RequestPermissions();
            if (isAuthorized)
            {
                tokenSource = new CancellationTokenSource();
                try
                {
                    RecognitionText = await _speechToText.Listen(CultureInfo.GetCultureInfo("en-GB"),
                        new Progress<string>(partialText =>
                        {
                            RecognitionText = partialText;
                        }), tokenSource.Token);


                    if (RecognitionText.ToLower().Contains("remove"))
                    {
                        List<Ingredient> selectedIngredients = MyKitchen.Where(x => RecognitionText.ToLower().Contains(x.IngredientName.ToLower())).ToList();
                        foreach (var item in selectedIngredients)
                        {
                            await TextToSpeech.SpeakAsync($"Remove the {item.IngredientName} from the kitchen");
                            MyKitchen.Remove(item);
                            Ingredients.Add(item);
                            await RemoveIngredientFromKitchenAsync(Tuple.Create(UserData.UserId, item.IngredientId));
                            if (item.IngredientCategory.Uid == SelectedTab.TabUid || SelectedTab.TabUid.Contains("All Ingredients"))
                                IngredientsBySelectedTab.Add(item);
                        }
                    }
                    else
                    {
                        List<Ingredient> ingredient = Ingredients.Where(x => RecognitionText.ToLower().Contains(x.IngredientName.ToLower())).ToList();
                        if (ingredient is not null && ingredient.Count > 0)
                        {
                            await GetIngredient(ingredient);
                        }
                        else
                        {
                            await TextToSpeech.SpeakAsync($"There is no such ingredient");
                        }

                    }

                }
                catch (Exception ex)
                {
                    await TextToSpeech.SpeakAsync($"You didn't choose an ingredient");
                }
            }
        }

        [RelayCommand]
        async Task GetDescriptionAsync(Ingredient ingredient)
        {
            IsLongPress = true;
            try
            {

                if (ingredient is not null)
                {
                    await MopupService.Instance.PushAsync(new IngredientDescriptionPopup(ingredient));
                }
            }
            catch (ArgumentException)
            {
                await MopupService.Instance.PopAsync();
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.httpStatusCodeNotFound);
            }
            catch (WebException)
            {
                await MopupService.Instance.PopAsync();
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.httpStatusCodeNotFound);
            }
            catch (Exception ex)
            {
                await MopupService.Instance.PopAsync();
                await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.UnknownError);
            }
        }

        [RelayCommand]
        void ListenCancel()
        {
            tokenSource?.Cancel();
        }

        [RelayCommand]
        async Task CloseAppAsync()
        {
            var isDecidedToBuy = await Shell.Current.DisplayAlert("Alert", $"Are you sure you want to exit app?", "Yes", "No");
            if (isDecidedToBuy)
                Application.Current.Quit();
        }

        [RelayCommand]
        void TabChanged(TabViewModel tab)
        {
            IngredientsBySelectedTab.Clear();

            var _ingredientsBySelectedTab = IngredientsWithoutKitchen.Where(x => x.IngredientCategory.Uid == tab.TabUid || tab.TabUid.Contains("All Ingredients")).ToList();

            foreach (var item in _ingredientsBySelectedTab)
            {
                IngredientsBySelectedTab.Add(item);
            }

            Tabs.All((arg) =>
            {
                if (arg.TabUid == tab.TabUid)
                {
                    arg.IsSelected = true;
                }
                else
                {
                    arg.IsSelected = false;
                }
                return true;
            });

        }

        [RelayCommand]
        void TabChangedTypeOfDish(TabViewModel tab)
        {
            if (tab is not null)
            {
                TabsTypeOfDish.All((arg) =>
                {
                    if (arg.TabUid == tab.TabUid)
                    {
                        arg.IsSelected = true;
                    }
                    else
                    {
                        arg.IsSelected = false;
                    }
                    return true;
                });
            }
            SelectedTabTypeOfDish = TabsTypeOfDish.FirstOrDefault(t => t.IsSelected == true);
        }

        [RelayCommand]
        async Task GetRecipeAsync(List<Ingredient> selectedIngredients)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayExceptionMessage.DisplaySnack("Please check your interner and try again.");
                return;
            }
            await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering recipe\n Please wait a few seconds..."));
            try
            {
                string ingredients;
                string result = null;
                string selectedTypeOfDish = SelectedTabTypeOfDish.TabTitle == "Anything" ? "" : SelectedTabTypeOfDish.TabTitle;
                if (selectedIngredients.Count() > 0)
                {
                    //ingredients = $"Give me one {selectedTypeOfDish} culinary recipe by combining these ingredients: {string.Join(',', selectedIngredients.Select(x => x.IngredientNameEng))}";
                    //result = await _openAIService.GetAnswerAsync(ingredients);
                }
                else
                {
                    if (MyKitchen.Count > 2)
                    {
                        //ingredients = $"Give me one {SelectedTabTypeOfDish.TabTitle} culinary recipe by combining these ingredients: {string.Join(',', MyKitchen.Select(x => x.IngredientNameEng))}";
                        //result = await _openAIService.GetAnswerAsync(ingredients);
                    }
                    else
                    {
                        result = "Not enough ingredients for any recipe.";
                        _culinaryRecipeViewModel.IsSaveEnabled = false;
                    }
                }

                await MopupService.Instance.PopAllAsync();
                if (string.IsNullOrEmpty(result))
                {
                    _culinaryRecipeViewModel.IsSaveEnabled = false;
                    await DisplayExceptionMessage.DisplaySnack("ChatGPT is busy, please try again later.");
                    return;
                }
                _culinaryRecipeViewModel.Recipe = result;
                _culinaryRecipeViewModel.TypeOfDish = SelectedTabTypeOfDish.TabUid;
                await MopupService.Instance.PushAsync(new CulinaryRecipePopup(_culinaryRecipeViewModel));
            }
            catch (WebException ex)
            {
                _culinaryRecipeViewModel.IsSaveEnabled = false;
                await DisplayExceptionMessage.DisplaySnack("ChatGPT is busy, please try again later.");
                await MopupService.Instance.PopAllAsync();
            }
            catch (Exception)
            {
                _culinaryRecipeViewModel.IsSaveEnabled = false;
                throw;
            }

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

        private async void LoadSeedDataAsync()
        {
            //IEnumerable<int> currentKitchen = null;
            //string selectedIngredients = await SecureStorage.GetAsync("Kitchen");
            //if (selectedIngredients != "")
            //{
            //    currentKitchen = selectedIngredients.Split(',').Select(int.Parse);
            //}

            //IEnumerable<IngredientCategory> ingredientCategories = SeedData.GetIngredientCategories();

            ////var allIngredients = SeedData.GetIngredients().ToList();
            ////GetYourKitchen(currentKitchen, allIngredients);


            ////AllIngredients = allIngredients.Where(x => (selectedIngredients == null || selectedIngredients.Count() == 0) || !currentKitchen.Contains(x.IngredientId)).ToList();
            //var selectedTab = GetTabs(ingredientCategories);

            //SelectedTab = selectedTab;
        }

        async Task GetIngredient(List<Ingredient> ingredients)
        {
            List<string> s = ingredients.Select(x => x.IngredientName).ToList();
            await TextToSpeech.SpeakAsync($"Add the {string.Join(' ', s)} to the kitchen");
            foreach (var ingredient in ingredients)
            {
                MyKitchen.Add(ingredient);
                Ingredients.Remove(ingredient);
                IngredientsBySelectedTab.Remove(ingredient);
                await SetIngredientAsync(Tuple.Create(UserData.UserUid, ingredient.Uid));
            }
        }

        public void FilterIngredients()
        {
            IngredientsBySelectedTab.Clear();
            var _ingredientsBySelectedTab = IngredientsWithoutKitchen
                .Where(x => (x.IngredientCategory.Uid == _selectedTab.TabUid || _selectedTab.TabUid.Contains("All Ingredients")) && x.IngredientName.ToLower()
                .Contains(SearchIngredient.ToLower())).ToList();

            foreach (var item in _ingredientsBySelectedTab)
            {
                IngredientsBySelectedTab.Add(item);
            }
        }

        public async Task SelectedIngredientAsync(Ingredient ingredient)
        {
            SearchIngredient = "";
            if (!IsLongPress)
            {
                if (UserData.EmailAddress is null)
                {
                    await DisplayExceptionMessage.DisplaySnack(ExceptionMessagesEnum.MustBeLoggedIn);
                    return;
                }
                MyKitchen.Add(ingredient);
                IngredientsWithoutKitchen.Remove(ingredient);
                IngredientsBySelectedTab.Remove(ingredient);


                await SetIngredientAsync(Tuple.Create(UserData.UserUid, ingredient.Uid));
            }
            IsLongPress = false;
        }

        [RelayCommand]
        public async Task RemoveIngredientAsync(Ingredient ingredient)
        {
            var result = await _alertService.ShowConfirmationAsync($"Remove {ingredient.IngredientName} from kitchen", $"Are you sure you want to remove {ingredient.IngredientName}?", "Yes", "No");
            if (result)
            {
                MyKitchen.Remove(ingredient);
                IngredientsWithoutKitchen.Add(ingredient);
                await RemoveIngredientFromKitchenAsync(Tuple.Create(UserData.UserId, ingredient.IngredientId));
                if (ingredient.IngredientCategory.Uid == SelectedTab.TabUid || SelectedTab.TabUid.Contains("All Ingredients"))
                {
                    IngredientsBySelectedTab.Add(ingredient);
                }
            }
        }

        async Task SetIngredientAsync(Tuple<string, string> Uids)
        {
            await _ingredientService.SaveIngredientAsync(Uids);
        }

        async Task RemoveIngredientFromKitchenAsync(Tuple<int, int> Ids)
        {
            await _ingredientService.RemoveIngredientAsync(Ids);
        }

        internal void GetTabsTypeOfDish()
        {
            TabsTypeOfDish = new ObservableCollection<TabViewModel>();

            foreach (var cuisineType in CuisineTypes)
            {
                TabsTypeOfDish.Add(new TabViewModel { TabUid = cuisineType.Uid, TabTitle = cuisineType.CuisineName });
            }

            SelectedTabTypeOfDish = TabsTypeOfDish.FirstOrDefault(x => x.IsSelected = true);
        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            GetTabs(IngredientCategories);
            GetTabsTypeOfDish();
            SelectedTab = Tabs.First();
        }


        internal void GetIngredientsWithoutKitchen(IEnumerable<Ingredient> generalIngredients, IEnumerable<Ingredient> myIngredients)
        {
            //Get the IDs of all ingredients in ingredients2
            var myIngredientUids = new HashSet<string>(myIngredients.Select(i => i.Uid));

            // Filter the ingredients in ingredients1 that are not in ingredients2
            var ingredientsDifference = generalIngredients.Where(i => !myIngredientUids.Contains(i.Uid)).ToList();

            foreach (var item in ingredientsDifference)
            {
                IngredientsWithoutKitchen.Add(item);
                IngredientsBySelectedTab.Add(item);
            }
        }

        internal async Task GetYourKitchen(IEnumerable<Ingredient> myKitchen)
        {
            MyKitchen.Clear();
            if (myKitchen is not null)
            {
                foreach (var ingredient in myKitchen)
                {
                    MyKitchen.Add(ingredient);
                }
            }
        }

        internal async Task OnAppearingAsync()
        {
            try
            {
                if (UserName is null)
                {
                    await MopupService.Instance.PushAsync(new SpinnerPopup("Prepering environment\n Please wait a few seconds..."));
                    UserName = UserData.UserName;
                    await GetYourKitchen(UserData.Ingredients);
                    GetTabsTypeOfDish();
                    GetIngredientsWithoutKitchen(Ingredients, UserData.Ingredients);
                    SelectedTab = GetTabs(IngredientCategories);

                }
            }
            catch (Exception)
            {
                await MopupService.Instance.PopAllAsync();
                throw;
            }
            finally
            {
                await MopupService.Instance.PopAllAsync();
            }
        }
        #endregion
    }
}
