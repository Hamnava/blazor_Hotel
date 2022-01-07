using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly HttpClient _client;
        public StripePaymentService(HttpClient client)
        {
            _client = client;
        }
        public async Task<SucessModel> CheckOut(StripePaymentDTO model)
        {
            var content = JsonConvert.SerializeObject(model);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/StripePayment/create", bodyContent);

            //string res = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SucessModel>(contentTemp);
                return result;
            }
            else
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(contentTemp);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
