using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;

namespace GarageManagement.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
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
            {
                await Shell.Current.DisplayAlert("Login Failed", "Không được bỏ trống username hay password", "OK");
                return;
            }
            var isauthenticated = await _authenticationservice.Authentication(username, password);
            if (isauthenticated && _authenticationservice.CurrentRole is not null)
            {
                var toast=Toast.Make("Đăng nhập thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                if (_authenticationservice.CurrentRole == "Admin")
                {
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    await toast.Show();
                }
                else if (_authenticationservice.CurrentRole == "User")
                {
                    await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}");
                    await toast.Show(); 
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Login Failed", "Tên đăng nhập hoặc mật khẩu không đúng", "OK");
                return;
            }
        }
    }
}
