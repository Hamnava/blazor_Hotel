using Models;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userRequest);
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userLogin);
        Task LogOut();
    }
}
