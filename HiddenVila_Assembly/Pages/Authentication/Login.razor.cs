using HiddenVila_Assembly.Services.IServices;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace HiddenVila_Assembly.Pages.Authentication
{
    public partial class Login
    {
        public AuthenticationDTO authenticationuser = new AuthenticationDTO();
        public bool IsProccessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }
        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        public NavigationManager navigatioinManager { get; set; }

        private async Task LoginUser()
        {
            IsProccessing = true;
            ShowAuthenticationErrors = false;
            var result = await authenticationService.Login(authenticationuser);
            if (result.IsAuthSuccessful)
            {
                IsProccessing = false;
                var absolutUri = new Uri(navigatioinManager.Uri);
                var queryparam = HttpUtility.ParseQueryString(absolutUri.Query);
                ReturnUrl = queryparam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigatioinManager.NavigateTo("/");
                }
                else
                {
                    navigatioinManager.NavigateTo("/" + ReturnUrl);
                }

            }
            else
            {
                IsProccessing = false;
                ShowAuthenticationErrors = true;
                Errors = result.ErrorMessage;
            }

        }
    }
}
