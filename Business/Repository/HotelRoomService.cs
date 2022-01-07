using AutoMapper;
using Business.Repository.Interfaces;
using DataAccess.Data;
using DataAccess.Enttities;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class HotelRoomService : IHotelRoom
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HotelRoomService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
        {
            var room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
            room.CreatedDate = DateTime.Now;
            room.CreatedBy = "";
           var addRoom = await _context.HotelRooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return _mapper.Map<HotelRoom, HotelRoomDTO>(addRoom.Entity);
        }

        public async Task<HotelRoomDTO> ExistRoomName(string roomName, int roomid = 0)
        {
            try
            {
                if (roomid == 0)
                {
                    var hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _context.HotelRooms.FirstOrDefaultAsync(r => r.Name == roomName));

                    return hotelRoom;
                }
                else
                {
                    var hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                    await _context.HotelRooms.FirstOrDefaultAsync(r => r.Name == roomName
                    && r.Id != roomid));

                    return hotelRoom;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int roomid, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(
                await _context.HotelRooms.Include(x=> x.HotelImages).FirstOrDefaultAsync(r => r.Id == roomid ));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    hotelRoom.IsBooked = await IsRoomBooked(roomid, checkInDateStr, checkOutDateStr);
                }

                return hotelRoom;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int RoomId, string checkInDatestr, string checkOutDatestr)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkOutDatestr) && !string.IsNullOrEmpty(checkInDatestr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDatestr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDatestr, "MM/dd/yyyy", null);

                    var existingBooking = await _context.RoomOrderDetails.Where(x => x.RoomId == RoomId && x.IsPaymentSuccessful &&
                       //check if checkin date that user wants does not fall in between any dates for room that is booked
                       ((checkInDate < x.CheckOutDate && checkInDate.Date >= x.CheckInDate)
                       //check if checkout date that user wants does not fall in between any dates for room that is booked
                       || (checkOutDate.Date > x.CheckInDate.Date && checkInDate.Date <= x.CheckInDate.Date)
                       )).FirstOrDefaultAsync();

                    if (existingBooking != null)
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<IEnumerable<HotelRoomDTO>> GetRooms(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                var roomList =
                  _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>
                  ( _context.HotelRooms.Include(x=> x.HotelImages));

                if (!string.IsNullOrEmpty(checkInDateStr) && !string.IsNullOrEmpty(checkOutDateStr))
                {
                    foreach (HotelRoomDTO hotelRoom in roomList)
                    {
                       hotelRoom.IsBooked = await IsRoomBooked(hotelRoom.Id, checkInDateStr, checkOutDateStr);
                    }
                }

                return roomList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> RemoveHotelRoom(int id)
        {
            try
            {
                var room = await _context.HotelRooms.FindAsync(id);
                if (room != null)
                {
                    var allImage = _context.HotelImagesUrls.Where(x => x.RoomId == id).ToList();
                    _context.HotelImagesUrls.RemoveRange(allImage);
                    _context.HotelRooms.Remove(room);
                     return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int roomid, HotelRoomDTO hotelRoomDTO)
        {
            try
            {
                if (roomid != 0)
                {
                    HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(roomid);
                    HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, hotelRoom);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updateroom = _context.HotelRooms.Update(room);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<HotelRoom,HotelRoomDTO>(updateroom.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
