﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;

namespace GarageManagement.ViewModels
{
    public partial class LoginPageViewModel: BaseViewModel
    {
        private readonly AuthenticationService _authenticationservice;
        public LoginPageViewModel(AuthenticationService authenticationService)
        {
            _authenticationservice = authenticationService;
        }
        [ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [RelayCommand]
        public async Task Login()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) 
                await Shell.Current.DisplayAlert("Login Failed", "Invalid Username or Password", "OK");
            var isauthenticated=await _authenticationservice.Authentication(username, password);
            if (isauthenticated && _authenticationservice.CurrentRole is not null)
            {
                if (_authenticationservice.CurrentRole=="Admin")
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                else if (_authenticationservice.CurrentRole=="Member") 
                    await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}");
            }
            else await Shell.Current.DisplayAlert("Login Failed", "Invalid Username or Password", "OK");
        }
    }
}
