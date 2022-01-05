using AutoMapper;
using Business.Repository.Interfaces;
using Common;
using DataAccess.Data;
using DataAccess.Enttities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class RoomOrderDetailsService : IRoomOrderDetails
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RoomOrderDetailsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomOrderDetailsDTO> Create(RoomOrderDetailsDTO details)
        {
            try
            {
                details.CheckInDate = details.CheckInDate.Date;
                details.CheckOutDate = details.CheckOutDate.Date;
                var roomMap = _mapper.Map<RoomOrderDetailsDTO, RoomOrderDetails>(details);
                details.Status = CD.Status_pending;
                var orderRoom = await _context.RoomOrderDetails.AddAsync(roomMap);
                await _context.SaveChangesAsync();
                return _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(orderRoom.Entity);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int orderRoomId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoomOrderDetailsDTO>> GetRoomOrderDetails()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRoomBooked(int roomId, DateTime checkIndate, DateTime checkOutdate)
        {
            throw new NotImplementedException();
        }

        public Task<RoomOrderDetailsDTO> MarkPaymentIsSuccessful(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatus(int OrderRoomId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
