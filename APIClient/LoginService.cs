using APIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIClient
{
    public class LoginService
    {
        private readonly HttpClient httpclient;
        private string endpoint;
        public LoginService(APIClientOption option, string endpoint)
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress = new System.Uri(option.APIBaseAddress);
            this.endpoint = endpoint;
        }
        public async Task<LoginResponseModel> Login(LoginRequestModel request)
        {
            var response = await httpclient.PostAsJsonAsync($"{endpoint}/Login", request);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<LoginResponseModel>();
        }
    }
}
