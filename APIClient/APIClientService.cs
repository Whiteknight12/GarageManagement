using APIClient.Models;
using APIClient.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIClient
{
    public class APIClientService<T> where T : class
    {
        private readonly HttpClient httpclient;
        private string endpoint; 
        public APIClientService(APIClientOption option, string endpoint) 
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress=new System.Uri(option.APIBaseAddress);
            this.endpoint = endpoint;
        }
        public async Task<List<T>> GetAll()
        {
            return await httpclient.GetFromJsonAsync<List<T>?>($"{endpoint}");
        }
        public async Task<T> GetByID(int id)
        {
            return await httpclient.GetFromJsonAsync<T?>($"{endpoint}/{id}");
        }
        public async Task Create(T item)
        {
            await httpclient.PostAsJsonAsync($"{endpoint}", item);
        }
        public async Task Update(T item)
        {
            await httpclient.PutAsJsonAsync($"{endpoint}", item);
        }
        public async Task Delete(int id)
        {
            await httpclient.DeleteAsync($"{endpoint}/{id}");
        }
    }
}
