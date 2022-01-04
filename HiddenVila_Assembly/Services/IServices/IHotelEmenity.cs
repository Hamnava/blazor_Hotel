using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IHotelEmenity
    {
        public Task<IEnumerable<HotelEmenityDTO>> GetAllEemenities();
    }
}
