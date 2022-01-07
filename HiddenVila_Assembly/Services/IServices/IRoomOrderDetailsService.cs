using Models;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IRoomOrderDetailsService
    {
        public Task<RoomOrderDetailsDTO> SaveRoomOrderDetails(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkPaymentSuccessful(RoomOrderDetailsDTO details);
    }
}
