using CommunityToolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.Services
{
    public class CuisineTypeService : ICuisineTypeService
    {
        public async Task<IEnumerable<CuisineType>> GetCuisineTypesAsync()
        {
            HttpClientHandler insecureHandler = ExtensionMethods.GetInsecureHandler();
            using (var httpClient = new HttpClient(insecureHandler))
            {
                try
                {
                    httpClient.BaseAddress = new Uri(App.BaseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "CuisineType/all");
                    var response = await httpClient.SendAsync(requestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CuisineType>>();
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
                return Enumerable.Empty<CuisineType>();
            }
        }
    }
}
