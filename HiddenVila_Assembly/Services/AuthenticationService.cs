using Blazored.LocalStorage;
using Common;
using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{

    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userLogin)
        {
            var content = JsonConvert.SerializeObject(userLogin);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(CD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(CD.Local_UserDetails, result.User);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new AuthenticationResponseDTO { IsAuthSuccessful = true };
            }
            else
            {
               return result;
            }
        }

        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync(CD.Local_Token);
            await _localStorage.RemoveItemAsync(CD.Local_UserDetails);
            _client.DefaultRequestHeaders.Authorization = null;
        }
        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userRequest)
        {
            var content = JsonConvert.SerializeObject(userRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signup", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO { IsRegistrationSuccessful = true };
            }
            else
            {
                return result;
            }
        }
    }
}
