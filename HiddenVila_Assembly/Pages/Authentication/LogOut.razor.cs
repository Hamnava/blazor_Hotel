using HiddenVila_Assembly.Services.IServices;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Pages.Authentication
{
    public partial class LogOut
    {
        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        public NavigationManager navigatioinManager { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await authenticationService.LogOut();
              navigatioinManager.NavigateTo("/");
        }
    }
}
