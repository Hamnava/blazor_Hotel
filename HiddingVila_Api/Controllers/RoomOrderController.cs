using Business.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace HiddingVila_Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomOrderController : ControllerBase
    {
        private readonly IRoomOrderDetails _details;
        public RoomOrderController(IRoomOrderDetails details)
        {
            _details = details;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomOrderDetailsDTO roomOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = "An Error while create room order"
                });
            }
            else
            {
                var result = await _details.Create(roomOrder);
                return Ok(result);
            }
        }
    }
}
