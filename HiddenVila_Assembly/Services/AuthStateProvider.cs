using Blazored.LocalStorage;
using Common;
using HiddenVila_Assembly.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _cleint;
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(HttpClient cleint, ILocalStorageService localStroage)
        {
            _cleint = cleint;
            _localStorage = localStroage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(CD.Local_Token);
            if (token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _cleint.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "JwtAuthType")));
        }

        public void NotityLoggedInUser(string token)
        {
            var authenticateduser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "JwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticateduser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
