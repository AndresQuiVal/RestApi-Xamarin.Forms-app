using Newtonsoft.Json;
using RestAPIServiceApplication.Helpers;
using RestAPIServiceApplication.Models;
using RestAPIServiceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestAPIServiceApplication.Services
{
    public class ApiService
    {
        #region Properties
        public string UrlBase { get; set; } = "http://localhost:44307"; // no SSL 
        #endregion

        #region Constructors

        #endregion

        #region Methods
        public async Task<RegisterResponse> RegisterUserAsync(UserProfileModel model)
        {
            var httpClient = new HttpClient();

            var jsonObjectSerialized = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(jsonObjectSerialized);

            //HttpContent httpContent = new StringContent(
            //        jsonObjectSerialized, Encoding.UTF8,
            //        //"application/json");

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //httpClient.BaseAddress = new Uri("https://localhost:44366");

            var response = 
                await httpClient.PostAsync(
                $"{UrlBase}/api/Account/Register", httpContent);

            var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(
                await response.Content.ReadAsStringAsync());

            return registerResponse;
        }

        public async Task<UserProfileViewModel> LoginAsync(string userName, string password)
        {
            var httpClient = new HttpClient();

            HttpContent httpContent = new StringContent(
                    $"grant_type=password&username={userName}&password={password}");

            httpContent.Headers.ContentType =
                new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync($"{UrlBase}/Token", httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonConvertedResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);

            if (!response.IsSuccessStatusCode)
                return new UserProfileViewModel() { Token = jsonConvertedResponse };

            //httpContent = 
            //    new StringContent($"?Authorization=bearer {jsonConvertedResponse.AccessToken}");

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jsonConvertedResponse.AccessToken);

            response = await httpClient.GetAsync($"{UrlBase}/api/Account/UserInfo");
            jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonUserConvertedResponse = ConverterHelper.ToUserProfileViewModel(
                JsonConvert.DeserializeObject<UserProfileModel>(jsonResponse));

            if (response.IsSuccessStatusCode)
            {
                jsonUserConvertedResponse.Token = jsonConvertedResponse;
                return jsonUserConvertedResponse;
            }

            return null;

            //var responseUser = this.GetAsync<UserProfileModel>

        }

        public async Task<T> GetAsync<T>(
            string controller, 
            string method = "",
            AuthenticationHeaderValue auth = null)
        {
            HttpClient httpClient = new HttpClient();

            if (auth != null)
                httpClient.DefaultRequestHeaders.Authorization = auth;

            var response = await httpClient.GetAsync($"{UrlBase}/api/{controller}{method}");

            if (!response.IsSuccessStatusCode)
                return default;

            var httpContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(httpContent);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(
            string controller,
            T model = default,
            string method = "",
            AuthenticationHeaderValue auth = null)
        {
            HttpClient httpClient = new HttpClient();

            if(auth != null)
                httpClient.DefaultRequestHeaders.Authorization = auth;

            HttpContent httpContent;
            string objectSerialized = string.Empty;

            if (model != null)
                objectSerialized = JsonConvert.SerializeObject(model);

            httpContent = new StringContent(objectSerialized);
            httpContent.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PostAsync($"{UrlBase}/api/{controller}{method}", httpContent);

            return response;
        }

        public async Task<bool> PutAsync<T>(
            string controller, 
            T model,
            AuthenticationHeaderValue auth,
            string id = "",
            string method = "")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = auth;

            string objectSerialized = JsonConvert.SerializeObject(model);
            HttpContent httpContent = new StringContent(objectSerialized);

            httpContent.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            var response = await httpClient.PutAsync($"{UrlBase}/api/{controller}{method}{id}", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(
            string controller, int id,
            AuthenticationHeaderValue auth, string method = "")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = auth;

            var response = 
                await httpClient.DeleteAsync($"{UrlBase}/api/{controller}/{id}");

            return response.IsSuccessStatusCode;
        } 
        #endregion
    }
}
