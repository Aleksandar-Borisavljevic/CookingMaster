using CommunityToolkit.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CookingMaster.Services
{
    public class CulinaryRecipeService : ICulinaryRecipeService
    {
        public async Task<string> SaveRecipeAsync(CulinaryRecipeRequest culinaryRecipe)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(culinaryRecipe);


                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "CulinaryRecipe/createculinaryrecipe");
                    requestMessage.Content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");
                    var response = await httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();

                    else
                    {
                        bool httpStatusCodeNotFound = response.StatusCode == System.Net.HttpStatusCode.NotFound;
                        Guard.IsFalse(httpStatusCodeNotFound);
                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (WebException ex)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;

                }
            }
        }

        public async Task<IEnumerable<CulinaryRecipeResponse>> GetRecipesAsync(int UserId = 0)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"CulinaryRecipe/all");
                    var response = await httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CulinaryRecipeResponse>>();
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
                catch (Exception)
                {
                    throw;

                }
                return null;
            }
        }
    }
}
