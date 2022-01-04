using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Interfaces
{
    public interface IRoomOrderDetails
    {
        public Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details);
        public Task<RoomOrderDetailsDTO> MarkPaymentIsSuccessful(int Id);
        public Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int orderRoomId);
        public Task<IEnumerable<RoomOrderDetailsDTO>> GetRoomOrderDetails();
        public Task<bool> UpdateStatus(int OrderRoomId, string status);
        public Task<bool> IsRoomBooked(int roomId, DateTime checkIndate, DateTime checkOutdate);

    }
}
