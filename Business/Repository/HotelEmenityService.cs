using AutoMapper;
using Business.Repository.Interfaces;
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
    public class HotelEmenityService : IHotelEmenity
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HotelEmenityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HotelEmenityDTO> CreateHotelEmenity(HotelEmenityDTO hotelEmenityDTO)
        {
            var mapEmenity = _mapper.Map<HotelEmenityDTO, HotelEmenity>(hotelEmenityDTO);
            var addEmenity = await _context.HotelEmenities.AddAsync(mapEmenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelEmenity, HotelEmenityDTO>(addEmenity.Entity);
        }

        public async Task<HotelEmenityDTO> ExistEmenityName(string emenityName, int emenityId = 0)
        {
            try
            {
                if (emenityId == 0)
                {
                    var emenity = _mapper.Map<HotelEmenity, HotelEmenityDTO>(
                    await _context.HotelEmenities.FirstOrDefaultAsync(r => r.Name == emenityName));

                    return emenity;
                }
                else
                {
                    var emenity = _mapper.Map<HotelEmenity, HotelEmenityDTO>(
                    await _context.HotelEmenities.FirstOrDefaultAsync(r => r.Name == emenityName
                    && r.Id != emenityId));

                    return emenity;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<HotelEmenityDTO>> GetHotelEmenities()
        {
            try
            {
                var emeityList = _mapper.Map<IEnumerable<HotelEmenity>, IEnumerable<HotelEmenityDTO>>
                    ( _context.HotelEmenities);
                return emeityList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<HotelEmenityDTO> GetHotelEmenity(int emenityId)
        {
            var emenity = _mapper.Map<HotelEmenity, HotelEmenityDTO>
                    (await _context.HotelEmenities.FirstOrDefaultAsync(x=> x.Id == emenityId));
            return emenity;
        }

        public async Task<int> RemoveHotelEmenity(int emenityId)
        {
            var hotelEmenity = await _context.HotelEmenities.FindAsync(emenityId);
             _context.HotelEmenities.Remove(hotelEmenity);
            return await _context.SaveChangesAsync();
        }

        public async Task<HotelEmenityDTO> UpdateHotelEmenity(int emenityId, HotelEmenityDTO hotelEmenityDTO)
        {
            var hotelEmenity = await _context.HotelEmenities.FindAsync(emenityId);
            var mapEmenity = _mapper.Map<HotelEmenityDTO, HotelEmenity>(hotelEmenityDTO, hotelEmenity);
            var update =  _context.HotelEmenities.Update(mapEmenity);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelEmenity, HotelEmenityDTO>(update.Entity);
        }
    }
}
