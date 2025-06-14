using APIClassLibrary.APIModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace GarageManagement.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpclient;
        private taiKhoanSession currentaccount;
        private DateTime expireDate; 
        private string STORAGE_KEY="user-account-status";

        public AuthenticationService(string baseaddress)
        {
            _httpclient = new HttpClient();
            _httpclient.BaseAddress = new Uri(baseaddress);
        }

        public async Task<bool> Authentication(string username, string password)
        {
            try
            {
                var result = false;
                var response = await _httpclient.PostAsJsonAsync("/api/Account/Login", new
                {
                    Username = username,
                    Password = password
                });
                if (response is not null && response.IsSuccessStatusCode)
                {
                    currentaccount = await response.Content.ReadFromJsonAsync<taiKhoanSession>();
                    if (currentaccount is not null)
                    {
                        await SecureStorage.Default.SetAsync(STORAGE_KEY, JsonSerializer.Serialize(currentaccount));
                        return true;
                    }
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
                Console.WriteLine($"Error Content: {errorContent}");
                return result;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HttpRequestException: {httpEx.Message}");
                if (httpEx.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {httpEx.InnerException.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        public async Task FettaiKhoanSession()
        {
            var currentaccountjson=await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (currentaccountjson is not null) currentaccount=JsonSerializer.Deserialize<taiKhoanSession>(currentaccountjson);
             if (DateTime.UtcNow >= currentaccount.Expiry) Logout();
        }

        public void Logout()
        {
            SecureStorage.Default.Remove(STORAGE_KEY);
            currentaccount = null;
        }

        public taiKhoanSession GetCurrentAccountStatus=>currentaccount;
        public string CurrentRole => currentaccount.Role;
    }
}
