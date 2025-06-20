﻿using System.Net.Http.Json;
using System.Runtime.InteropServices;

namespace APIClassLibrary
{
    public class APIClientService<T> where T: class
    {
        private readonly HttpClient _httpclient;
        private readonly string _endpoint;
        private string STORAGE_KEY = "user-account-status";

        public APIClientService(APIClientOptions options, string endpoint)
        {
            _httpclient = new HttpClient();
            _httpclient.BaseAddress = new Uri(options.BaseAddress);
            _endpoint = endpoint;
            
        }
        
        public HttpClient GetHttpClient => _httpclient; 
        public async Task<List<T>> GetAll()
        {
            var response = await _httpclient.GetFromJsonAsync<List<T>>($"/api/{_endpoint}");

            return response;
        }
        public async Task<T?> GetByID(Guid id)
        {
            var response = await _httpclient.GetAsync($"/api/{_endpoint}/{id}");
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<T>();
            return null;
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
        public async Task Delete(Guid id)
        {
            await _httpclient.DeleteAsync($"/api/{_endpoint}/{id}");
        }
        public async Task<T?> GetThroughtSpecialRoute(string route, string parameter="")
        {

            var p = string.IsNullOrWhiteSpace(parameter)
        ? ""
        : $"/{Uri.EscapeDataString(parameter)}";

            // nếu route chính là "name" thì coi như nó là root
            bool isRoot = route.Equals("name", StringComparison.OrdinalIgnoreCase);

            string path = isRoot
                ? $"/{route}{p}"
                : $"/api/{_endpoint}/{route}{p}";

            var response = await _httpclient.GetAsync(path);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<List<T>?> GetListOnSpecialRequirement(string route)
        {
            var response = await _httpclient.GetAsync($"/api/{_endpoint}/{route}");
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<T>>();
            return null;
        }
    }
}
