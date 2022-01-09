using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } 

        [CascadingParameter]
        public Task<AuthenticationState> authenticationState { get; set; }

        public bool notAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri);
            var authstate = await authenticationState;

            if (authstate?.User?.Identity is null || !authstate.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("login", true);
                }
                else
                {
                    navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                notAuthorized = true;
            }
          
        }
    }
}
