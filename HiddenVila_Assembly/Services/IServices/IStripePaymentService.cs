using Models;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services.IServices
{
    public interface IStripePaymentService
    {
        public Task<SucessModel> CheckOut(StripePaymentDTO model);
    }
}
