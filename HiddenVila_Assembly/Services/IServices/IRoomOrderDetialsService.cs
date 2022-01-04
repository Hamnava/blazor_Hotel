using Models;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IRoomOrderDetialsService
    {
        public Task<RoomOrderDetailsDTO> SaveRooomOrderDetails(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkIsOrderSuccessful(RoomOrderDetailsDTO details);
    }
}
