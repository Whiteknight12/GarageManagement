using APIClient;
using MAUIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIProject.Services
{
    public class AuthenticationService
    {
        private UserAccountSession acc;
        private const string USER_SESSION_STORAGE_KEY = "useraccountsession";
        private readonly LoginService loginservice;
        public AuthenticationService(LoginService loginservice)
        {
            this.loginservice = loginservice;
        }
        public UserAccountSession currentuseraccount=> acc;
        public bool IsAdmin => acc is not null && acc.Role == "Admin";
        public async Task<bool> Authentication(string username, string password)
        {
            var result = false;
            var response = await loginservice.Login(new APIClient.Models.LoginRequestModel { Username = username, Password = password });
            if (response is not null)
            {
                acc.Username = response.Username;
                acc.UserToken = response.AccessToken;
                acc.Role = response.Role;
                await SecureStorage.Default.SetAsync(USER_SESSION_STORAGE_KEY, JsonSerializer.Serialize(acc));
                result = true;
            }
            return result;
        }
        public async Task FetchUserAccountSession()
        {
            var useraccountsessionjson=await SecureStorage.Default.GetAsync(USER_SESSION_STORAGE_KEY);
            if (!string.IsNullOrEmpty(useraccountsessionjson))
            {
                acc = JsonSerializer.Deserialize<UserAccountSession>(useraccountsessionjson);
            }
        }
        public void Logout()
        {
            SecureStorage.Default.Remove(USER_SESSION_STORAGE_KEY);
            acc = null;
        }
    }
}
