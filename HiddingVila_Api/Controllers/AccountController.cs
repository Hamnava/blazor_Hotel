using Common;
using DataAccess.Enttities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace HiddingVila_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(SignInManager<ApplicationUser> signInManager,
                                     UserManager<ApplicationUser> userManager,
                                           RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new ApplicationUser() 
            { 
              Name = userRequestDTO.Name,
              Email = userRequestDTO.Email,
              UserName = userRequestDTO.Email,
              EmailConfirmed = true,
              PhoneNumber = userRequestDTO.PhoneNo
            };

            var result = await _userManager.CreateAsync(user, userRequestDTO.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.Select(x => x.Description);
                return BadRequest(new RegistrationResponseDTO()
                {
                    Errors = error,
                    IsRegistrationSuccessful = false
                });
            }

            var roleResult =await _userManager.AddToRoleAsync(user,CD.Role_Customer);
            if (!roleResult.Succeeded)
            {
                var error = result.Errors.Select(x => x.Description);
                return BadRequest(new RegistrationResponseDTO()
                {
                    Errors = error,
                    IsRegistrationSuccessful = false
                });
            }
            return StatusCode(201);
        }
    }
}
