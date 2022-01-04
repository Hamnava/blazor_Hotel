using Business.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HiddingVila_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelEmenityController : ControllerBase
    {
        private readonly IHotelEmenity _emenity;
        public HotelEmenityController(IHotelEmenity emenity)
        {
            _emenity = emenity;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmenity()
        {
            var Emenities = await _emenity.GetHotelEmenities();
            return Ok(Emenities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmenity(int id)
        {
            var Emenity = await _emenity.GetHotelEmenity(id);
            return Ok(Emenity);
        }
    }
}
