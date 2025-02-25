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
    public class APIClientService
    {
        private readonly HttpClient httpclient;
        public APIClientService(APIClientOption option) 
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress=new System.Uri(option.APIBaseAddress);
        }
        public async Task<List<Car>> GetAllCars()
        {
            return await httpclient.GetFromJsonAsync<List<Car>?>("/api/Car");
        }
        public async Task<Car> GetCarByID(int id)
        {
            return await httpclient.GetFromJsonAsync<Car?>($"/api/Car/{id}");
        }
        public async Task CreateCar(Car car)
        {
            await httpclient.PostAsJsonAsync("/api/Car", car);
        }
        public async Task UpdateCar(Car car)
        {
            await httpclient.PutAsJsonAsync("/api/Car", car);
        }
        public async Task DeleteCar(int id)
        {
            await httpclient.DeleteAsync($"/api/Car/{id}");
        }
    }
}
