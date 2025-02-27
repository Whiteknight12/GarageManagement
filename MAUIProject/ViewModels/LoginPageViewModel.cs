using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIProject.ViewModels
{
    public partial class LoginPageViewModel: BaseViewModel
    {
        private readonly AuthenticationService authenticationService;
        [ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        public LoginPageViewModel(AuthenticationService authenticationservice)
        {
            this.authenticationService = authenticationservice;
        }
        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) 
                await Shell.Current.DisplayAlert("Error", "Username and password must not be empty", "OK");
            var Isauthenticated = await authenticationService.Authentication(username, password);
            if (Isauthenticated)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
            }
        }
    }
}
