﻿using APIClassLibrary.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpclient;
        private UserAccountSession currentaccount;
        private string STORAGE_KEY="user-account-status";
        public AuthenticationService(string baseaddress)
        {
            _httpclient = new HttpClient();
            _httpclient.BaseAddress = new Uri(baseaddress);
        }
        public async Task<bool> Authentication(string username, string password)
        {
            var result = false;
            var response=await _httpclient.PostAsJsonAsync("/api/Account/Login", new 
            {
                Username = username,
                Password = password
            });
            if (response is not null && response.IsSuccessStatusCode)
            {
                currentaccount = await response.Content.ReadFromJsonAsync<UserAccountSession>();
                if (currentaccount is not null)
                {
                    await SecureStorage.Default.SetAsync(STORAGE_KEY, JsonSerializer.Serialize(currentaccount));
                    return true;
                }
            }
            return result;
        }
        public async Task FetUserAccountSession()
        {
            var currentaccountjson=await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (currentaccountjson is not null) currentaccount=JsonSerializer.Deserialize<UserAccountSession>(currentaccountjson);
        }
        public void Logout()
        {
            SecureStorage.Default.Remove(STORAGE_KEY);
            currentaccount = null;
        }
        public UserAccountSession GetCurrentAccountStatus=>currentaccount;
        public string CurrentRole => currentaccount.Role;
    }
}
