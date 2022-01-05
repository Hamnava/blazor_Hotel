using HiddenVila_Assembly.Services.IServices;
using Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class RoomOrderDetialsService : IRoomOrderDetialsService
    {
        private readonly HttpClient _client;
        public RoomOrderDetialsService(HttpClient client)
        {
            _client = client;
        }
        public Task<RoomOrderDetailsDTO> MarkIsOrderSuccessful(RoomOrderDetailsDTO details)
        {
            throw new System.NotImplementedException();
        }

        public Task<RoomOrderDetailsDTO> SaveRooomOrderDetails(RoomOrderDetailsDTO details)
        {
            throw new System.NotImplementedException();
        }
    }
}
