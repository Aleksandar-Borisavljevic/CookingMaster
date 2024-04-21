using System.Security.Cryptography.X509Certificates;

namespace CookingMaster;

public partial class App : Application
{
    //public const string productionUrl = "https://cookingbookapi.azurewebsites.net/api/";
    public const string devUrl = "https://10.0.2.2:7275/api/";

    public static string BaseUrl { get; set; }
    public App()
	{
		InitializeComponent();
        BaseUrl = devUrl;
        MainPage = new AppShell();
	}
}
