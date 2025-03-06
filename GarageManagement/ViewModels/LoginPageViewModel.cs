using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            if (isauthenticated) await Shell.Current.GoToAsync("//MainPage");
            else await Shell.Current.DisplayAlert("Login Failed", "Invalid Username or Password", "OK");
        }
    }
}
