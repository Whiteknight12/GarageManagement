using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APIClassLibrary
{
    public class APIClientService<T> where T: class
    {
        private readonly HttpClient _httpclient;
        private readonly string _endpoint;
        public APIClientService(APIClientOptions options, string endpoint)
        {
            _httpclient = new HttpClient();
            _httpclient.BaseAddress = new Uri(options.BaseAddress);
            _endpoint = endpoint;
        }
        public async Task<List<T>> GetAll()
        {
            return await _httpclient.GetFromJsonAsync<List<T>>($"/api/{_endpoint}");
        }
        public async Task<T> GetByID(int id)
        {
            return await _httpclient.GetFromJsonAsync<T>($"/api/{_endpoint}/{id}");
        }
        public async Task Update(T entity)
        {
            await _httpclient.PutAsJsonAsync($"/api/{_endpoint}", entity);
        }
        public async Task<T> Create(T entity)
        {
            var response=await _httpclient.PostAsJsonAsync($"/api/{_endpoint}", entity);
            var obj = await response.Content.ReadFromJsonAsync<T>();
            return obj;
        }
        public async Task Delete(int id)
        {
            await _httpclient.DeleteAsync($"/api/{_endpoint}/{id}");
        }
        public async Task<T?> GetThroughtSpecialRoute(string route, string parameter="")
        {
            var response = await _httpclient.GetAsync($"/api/{_endpoint}/{route}/{parameter}");
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<T>();
            return null;
        }
        public async Task<List<T>?> GetListOnSpecialRequirement(string route)
        {
            var response = await _httpclient.GetAsync($"/api/{_endpoint}/{route}");
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<T>>();
            return null;
        }
    }
}
