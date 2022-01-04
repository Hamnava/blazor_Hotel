using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class HotelEmenityService : IHotelEmenity
    {
        private readonly HttpClient _client;
        public HotelEmenityService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<HotelEmenityDTO>> GetAllEemenities()
        {
            var response = await _client.GetAsync($"api/HotelEmenity");
            var content = await response.Content.ReadAsStringAsync();
            var emenity = JsonConvert.DeserializeObject<IEnumerable<HotelEmenityDTO>>(content);
            return emenity;
        }
    }
}
