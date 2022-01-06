﻿using HiddenVila_Assembly.Services.IServices;
using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVila_Assembly.Services
{
    public class RoomOrderDetialsService : IRoomOrderDetialsService
    {
        private readonly HttpClient _client;
        public RoomOrderDetialsService(HttpClient client)
        {
            _client = client;
        }
        public Task<RoomOrderDetailsDTO> MarkIsOrderSuccessful(RoomOrderDetailsDTO details)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RoomOrderDetailsDTO> SaveRooomOrderDetails(RoomOrderDetailsDTO details)
        {
            details.UserId = "Dummy user";
            var content = JsonConvert.SerializeObject(details);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/RoomOrder/create", bodyContent);

            string res =  response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var contentTemp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RoomOrderDetailsDTO>(contentTemp);
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
