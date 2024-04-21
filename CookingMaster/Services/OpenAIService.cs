using CommunityToolkit.Diagnostics;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CookingMaster.Services
{
    public class OpenAIService : IOpenAIService
    {
        public async Task<string> GetAnswerAsync(string question)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    // Set the timeout to 15 seconds
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(question);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "OpenAI");
                    requestMessage.Content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<string>();
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
