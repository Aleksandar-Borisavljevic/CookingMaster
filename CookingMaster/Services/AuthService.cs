using CommunityToolkit.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text;

namespace CookingMaster.Services
{
    internal class AuthService : IAuthService
    {
        public async Task<UserKitchen> LoginAsync(UserLogin userLogin)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(userLogin);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "auth/login");
                    requestMessage.Content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<UserKitchen>();
                        return result;
                    }
                    else
                    {
                        bool httpStatusCodeNotFound = response.StatusCode == System.Net.HttpStatusCode.NotFound;
                        Guard.IsFalse(httpStatusCodeNotFound);
                        return null;
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

        public async Task<string> RegisterAsync(UserRegister userRegister)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(userRegister);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "auth/register");
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
                catch (Exception ex)
                {
                    throw;

                }
            }
        }

        public async Task VerifyAsync(string verifyCode)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(verifyCode);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"auth/verify/?token={verifyCode}");

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

        public async Task<string> ForgotPasswordAsync(string email)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"auth/forgot-password/?email={email}");

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
                catch (Exception ex)
                {
                    throw;

                }
            }
        }

        public async Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string jsonQuestion = JsonConvert.SerializeObject(resetPassword);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"auth/reset-password");

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
