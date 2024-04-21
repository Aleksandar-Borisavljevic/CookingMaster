using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace CookingMaster;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .ConfigureMopups()
            .UseSkiaSharp()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("materialdesignicons-webfont.ttf", "IconFontTypes");
        }).ConfigureMauiHandlers(options => options.AddCompatibilityRenderers(typeof(Xamarin.CommunityToolkit.Effects.TouchEffect).Assembly))
        .ConfigureEffects(options =>
        {
            options.AddCompatibilityEffects(typeof(Xamarin.CommunityToolkit.Effects.TouchEffect).Assembly);
#if ANDROID
                // https://github.com/xamarin/XamarinCommunityToolkit/issues/1905#issuecomment-1254311606
                options.Add(typeof(Xamarin.CommunityToolkit.Effects.TouchEffect), typeof(Xamarin.CommunityToolkit.Android.Effects.PlatformTouchEffect));
#endif
        });



#if DEBUG
        builder.Logging.AddDebug();
#endif

#if ANDROID
        builder.Services.AddSingleton<ISpeechToText, Platforms.SpeechToTextService>();
#endif

        #region RegisterRoute
        Routing.RegisterRoute("IngredientDescriptionPopup", typeof(IngredientDescriptionPopup));
        Routing.RegisterRoute("RegisterPopup", typeof(RegisterPopup));
        Routing.RegisterRoute("ForgotPasswordPopup", typeof(ForgotPasswordPopup));
        #endregion

        #region Views
        builder.Services.AddTransient<MyKitchenPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MyCookBookPage>();
        builder.Services.AddTransient<HomePage>();
        #endregion

        #region Popups
        builder.Services.AddSingleton<IngredientDescriptionPopup>();
        builder.Services.AddSingleton<RegisterPopup>();
        builder.Services.AddSingleton<ForgotPasswordPopup>();
        #endregion

        #region ViewModels
        builder.Services.AddTransient<BaseViewModel>();
        builder.Services.AddTransient<MyKitchenViewModel>();
        builder.Services.AddSingleton<IngredientDescriptionViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<ForgotPasswordViewModel>();
        builder.Services.AddSingleton<CulinaryRecipeViewModel>();
        builder.Services.AddSingleton<MyCookBookViewModel>();
        builder.Services.AddTransient<HomeViewModel>();

        #endregion

        #region Services
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IIngredientCategoryService, IngredientCategoryService>();
        builder.Services.AddSingleton<IIngredientService, IngredientService>();
        builder.Services.AddSingleton<IIngredientDescriptionService, IngredientDescriptionService>();
        builder.Services.AddSingleton<IOpenAIService, OpenAIService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddSingleton<ICulinaryRecipeService, CulinaryRecipeService>();
        builder.Services.AddSingleton<ICuisineTypeService, CuisineTypeService>();
        #endregion

        return builder.Build();
    }
}