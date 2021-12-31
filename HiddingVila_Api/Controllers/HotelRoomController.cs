using Business.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace HiddingVila_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoom _rooms;
        public HotelRoomController(IHotelRoom roome)
        {
            _rooms = roome;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelRooms()
        {
            var allrooms = await _rooms.GetRooms();
            return Ok(allrooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId)
        {
            if (roomId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid RoomId",
                    ErrorTitle = "Faild",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var roomDetails = await _rooms.GetHotelRoom(roomId.Value);
            if (roomDetails == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid RoomId",
                    ErrorTitle = "Faild",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(roomDetails);

        }
    }
}
