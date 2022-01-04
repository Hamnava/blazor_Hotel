using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System;
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

        public async Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkoutDate)
        {
            var response = await _client.GetAsync($"api/HotelRoom/{roomId}?checkInDate={checkInDate}&checkOutDate={checkoutDate}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var room = JsonConvert.DeserializeObject<HotelRoomDTO>(content);
                return room;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
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
