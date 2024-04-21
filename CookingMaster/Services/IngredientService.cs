using System.Net.Http.Json;
using System.Net;
using CommunityToolkit.Diagnostics;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace CookingMaster.Services
{
    public class IngredientService : IIngredientService
    {
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"Ingredient/all");
                    var response = await httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Ingredient>>();

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

        public async Task SaveIngredientAsync(Tuple<string, string> Uids)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(Uids);

                    //TODO: refactor this line
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"Ingredient/createUserIngredient");
                    requestMessage.Content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                        return;

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
            }
        }

        public async Task RemoveIngredientAsync(Tuple<int, int> Ids)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(Ids);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"Ingredient/deleteUserIngredient");
                    requestMessage.Content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                        return;

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
            }
        }


    }
}
