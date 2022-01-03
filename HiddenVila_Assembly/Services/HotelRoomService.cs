using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly HttpClient _client;
        public HotelRoomService(HttpClient client)
        {
            _client = client; 
        }

        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkoutDate)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkoutDate)
        {
            var response = await _client.GetAsync($"api/HotelRoom?checkInDate={checkInDate}&checkOutDate={checkoutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
            return rooms;
        }
    }
}
