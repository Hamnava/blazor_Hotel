using AutoMapper;
using Business.Repository.Interfaces;
using Common;
using DataAccess.Data;
using DataAccess.Enttities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<RoomOrderDetailsDTO> GetRoomOrderDetails(int orderRoomId)
        {
            try
            {
                RoomOrderDetails orderDetails = await _context.RoomOrderDetails
                    .Include(x => x.HotelRoom).ThenInclude(x => x.HotelImages)
                    .FirstOrDefaultAsync(x => x.Id == orderRoomId);
                RoomOrderDetailsDTO roomOrderDetailsDTO = _mapper.Map<RoomOrderDetails, RoomOrderDetailsDTO>(orderDetails);
                roomOrderDetailsDTO.HotelRoomDTO.TotalDays = roomOrderDetailsDTO.CheckOutDate.Subtract(roomOrderDetailsDTO.CheckInDate).Days;
                return roomOrderDetailsDTO;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<IEnumerable<RoomOrderDetailsDTO>> GetRoomOrderDetails()
        {
            try
            {
                IEnumerable<RoomOrderDetailsDTO> roomOrders = _mapper.Map<IEnumerable<RoomOrderDetails>, IEnumerable<RoomOrderDetailsDTO>>
               (_context.RoomOrderDetails.Include(x => x.HotelRoom));

                return roomOrders;
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime checkIndate, DateTime checkOutdate)
        {
            var status = false;
            var existingRoom =await _context.RoomOrderDetails.Where(x => x.RoomId == roomId && x.IsSuccessFulPayment == true &&
            // check if checkin date that user wants does not fall between any dates for room that is booked
            checkIndate.Date > x.CheckInDate && checkIndate.Date < checkOutdate ||
            // check if chekcout date that user wants does not fall between any dates for room that is booked
            checkOutdate.Date > x.CheckInDate && checkIndate.Date < x.CheckOutDate
            ).FirstOrDefaultAsync();

            if (existingRoom != null) status = true;

            return status;
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
