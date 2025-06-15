using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GarageManagement.ViewModels
{
    public partial class SuaTaiKhoanPageViewModel: BaseViewModel
    {
        public Guid taiKhoanId;

        [ObservableProperty]
        private string tenDangNhap;

        [ObservableProperty]
        private string matKhau;

        private readonly APIClientService<TaiKhoan> _accountService;

        public SuaTaiKhoanPageViewModel(APIClientService<TaiKhoan> accountService)
        {
            _accountService = accountService;
        }

        public async Task LoadAsync()
        {
            var result = await _accountService.GetByID(taiKhoanId);
            TenDangNhap = string.Empty;
            MatKhau = string.Empty;
            if (result is not null)
            {
                TenDangNhap = result.TenDangNhap;
                MatKhau = result.MatKhau;
            }
        }

        [RelayCommand]
        public async Task UpdateAccount()
        {
            if (string.IsNullOrEmpty(TenDangNhap))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống tên đăng nhập", "OK");
                return;
            }
            if (string.IsNullOrEmpty(MatKhau))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống mật khẩu", "OK");
                return;
            }
            var listTK=await _accountService.GetAll();
            if (listTK is not null)
            {
                foreach (var item in listTK)
                {
                    if (item.TenDangNhap==TenDangNhap)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Tên đăng nhập đã tồn tại", "OK");
                        return;
                    }
                }
            }
            var result = await _accountService.GetByID(taiKhoanId);
            if (result is not null)
            {
                result.TenDangNhap = TenDangNhap;
                result.MatKhau = MatKhau;
                await _accountService.Update(result);
                var toast = Toast.Make("Cập nhật tài khoản thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
                MessagingCenter.Send(this, "UpdateAccountList");
            }
        }
    }
}
