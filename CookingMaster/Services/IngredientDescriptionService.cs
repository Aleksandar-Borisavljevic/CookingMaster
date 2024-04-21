using System.Net.Http.Json;
using System.Net;
using CommunityToolkit.Diagnostics;
using System.Net.Http.Headers;

namespace CookingMaster.Services
{
    public class IngredientDescriptionService : IIngredientDescriptionService
    {
        public async Task<IngredientDescription> GetIngredientDescriptionAsync(int ingredientUid)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"IngredientNutrient/{ingredientUid}");
                    var response = await httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<IngredientDescription>();
                        return result;
                    }
                    else
                    {
                        bool httpStatusCodeNotFound = response.StatusCode == System.Net.HttpStatusCode.NotFound;
                        Guard.IsFalse(httpStatusCodeNotFound);
                    }
                }
                catch (WebException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;

                }
                return null;
            }
        }
    }
}
