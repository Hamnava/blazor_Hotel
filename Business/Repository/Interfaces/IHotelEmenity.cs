using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.Interfaces
{
    public interface IHotelEmenity
    {
        Task<HotelEmenityDTO> CreateHotelEmenity(HotelEmenityDTO hotelEmenityDTO);
        Task<HotelEmenityDTO> UpdateHotelEmenity(int emenityId,HotelEmenityDTO hotelEmenityDTO);
        Task<HotelEmenityDTO> GetHotelEmenity(int emenityId);
        Task<IEnumerable<HotelEmenityDTO>> GetHotelEmenities();
        Task<int> RemoveHotelEmenity(int emenityId);
    }
}
