using HiddenVila_Assembly.Services.IServices;
using Microsoft.AspNetCore.Components;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Pages.Authentication
{
    public partial class Registration
    {
        private UserRequestDTO requestForUser = new UserRequestDTO();
        public bool IsProccessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }
        
         [Inject]
         public IAuthenticationService authenticationService { get; set; }
        
         [Inject]
         public  NavigationManager navigatioinManager { get; set; }

        private async Task RegisterUser()
        {
            IsProccessing = true;
            ShowRegistrationErrors = false;
            var result = await authenticationService.RegisterUser(requestForUser);
            if (result.IsRegistrationSuccessful)
            {
                IsProccessing = false;
                navigatioinManager.NavigateTo("/login");
            }
            else
            {
                IsProccessing = false;
                ShowRegistrationErrors = true;
                Errors = result.Errors;
            }

        }
    }
}
