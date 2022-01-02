using Business.Repository.Interfaces;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Globalization;
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
        [Authorize(Roles = CD.Role_Admin)]
        public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate)|| string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be suplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "dd/MM/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid check In date format, Valid format will be MM/dd/yyyy"
                });
            }
            if (!DateTime.TryParseExact(checkOutDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid check Out date format, Valid format will be MM/dd/yyyy"
                });
            }
            var allrooms = await _rooms.GetRooms(checkInDate, checkOutDate);
            return Ok(allrooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
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
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be suplied"
                });
            }
            if (!DateTime.TryParseExact(checkInDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid check In date format, Valid format will be MM/dd/yyyy"
                });
            }
            if (!DateTime.TryParseExact(checkOutDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dtCheckOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid check Out date format, Valid format will be MM/dd/yyyy"
                });
            }
            var roomDetails = await _rooms.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);
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
