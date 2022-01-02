

using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IHotelRoom
    {
        public Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkoutDate);
        public Task<HotelRoomDTO> GetHotelRoomDetails(int roomId,string checkInDate, string checkoutDate);
    }
}
