using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;

namespace GarageManagement.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly AuthenticationService _authenticationService;
        public LoginPageViewModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            Init(); 
        }
        [ObservableProperty]
        private string username = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private bool isRememberMe;

        public async void Init()
        {
            if (Preferences.Default.Get("remember_me", false))
            {
                IsRememberMe = true;
                Username = Preferences.Default.Get("saved_username", "");
                // password nằm trong SecureStorage
                Password = await SecureStorage.Default.GetAsync("saved_password") ?? "";
            }
        }

        [RelayCommand]
        public async Task Login()
        {
            var ok = await _authenticationService.Authentication(Username, Password);
            if (!ok || _authenticationService.CurrentRole is null)
            {
                await Shell.Current.DisplayAlert("Login Failed", "Tên đăng nhập hoặc mật khẩu không đúng", "OK");
                return;
            }

            // nếu user chọn "Nhớ mật khẩu", lưu lại
            if (IsRememberMe)
            {
                Preferences.Default.Set("remember_me", true);
                Preferences.Default.Set("saved_username", Username);
                await SecureStorage.Default.SetAsync("saved_password", Password);
            }
            else
            {
                // xoá nếu trước đây có lưu
                Preferences.Default.Remove("remember_me");
                Preferences.Default.Remove("saved_username");
                SecureStorage.Default.Remove("saved_password");
            }

            // điều hướng vào app
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
