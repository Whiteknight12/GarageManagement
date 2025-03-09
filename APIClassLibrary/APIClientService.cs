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
        public async Task Create(T entity)
        {
            await _httpclient.PostAsJsonAsync($"/api/{_endpoint}", entity);
        }
        public async Task Delete(int id)
        {
            await _httpclient.DeleteAsync($"/api/{_endpoint}/{id}");
        }
        public async Task<T> GetThroughtSpecialRoute(string route, string parameter="")
        {
            return await _httpclient.GetFromJsonAsync<T>($"/api/{_endpoint}/{route}/{parameter}");
        }
    }
}
