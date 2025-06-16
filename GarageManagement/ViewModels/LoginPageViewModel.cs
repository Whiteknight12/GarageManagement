using APIClassLibrary;
using APIClassLibrary.APIModels;
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
        private readonly IEmailService _emailService;
        private readonly APIClientService<TaiKhoan> _tk;
        private readonly APIClientService<KhachHang> _kh;
        private readonly APIClientService<NhanVien> _nv; 
        public LoginPageViewModel(AuthenticationService authenticationService, IEmailService emailService, APIClientService<TaiKhoan> tk, APIClientService<KhachHang> kh, APIClientService<NhanVien> nv)
        { 
            _kh = kh;
            _authenticationService = authenticationService;
            _tk = tk;
            Init();
            _emailService = emailService;
            _nv = nv;
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

        [RelayCommand]
        private async void ForgotPassword()
        {
            var tks = await _tk.GetAll();
            var khs = await _kh.GetAll();
            var nvs = await _nv.GetAll();
            var tk = tks.FirstOrDefault(x => x.TenDangNhap == Username);
            if (tk is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Sai tên đăng nhập", "OK");
                return;
            }
            var kh = khs.FirstOrDefault(kh => kh.TaiKhoanId == tk.Id); 
            if (kh != null)
            {
                if(!string.IsNullOrEmpty(kh.Email))
                {
                    await _emailService.SendPasswordResetAsync(kh.Email, "https://localhost:7228/api/Account/reset-password"+"/"+tk.TenDangNhap); // Thay đổi link reset mật khẩu theo yêu cầu
                }
                else
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Thông tin khách hàng này không có email để gửi link reset mật khẩu", "OK");
                }
            }
            var nv = nvs.FirstOrDefault(nv => nv.TaiKhoanId == tk.Id);
            if (nv != null)
            {
                if (!string.IsNullOrEmpty(nv.Email))
                {
                    await _emailService.SendPasswordResetAsync(nv.Email, "https://localhost:7228/api/Account/reset-password" + "/" + tk.TenDangNhap); // Thay đổi link reset mật khẩu theo yêu cầu
                }
                else
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Thông tin nhân viên này không có email để gửi link reset mật khẩu", "OK");
                }
            }
        }
    }
}
