using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChangePasswordPageViewmodel : BaseViewModel
    {
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;
        public Guid id;
        private readonly APIClientService<TaiKhoan> _taiKhoanService;
        public ChangePasswordPageViewmodel(APIClientService<TaiKhoan> tkS)
        {
            _taiKhoanService = tkS;
        }

        [RelayCommand]
        private async void SaveChange()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
                return;
            }
            var taiKhoan = await _taiKhoanService.GetByID(id);
            if (taiKhoan != null)
            {
                if(taiKhoan.TenDangNhap == Username && taiKhoan.MatKhau == Password)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Không có thay đổi nào được thực hiện", "OK");
                    return;
                }
                taiKhoan.TenDangNhap = Username;
                taiKhoan.MatKhau = Password;
                await _taiKhoanService.Update(taiKhoan);
                await Shell.Current.DisplayAlert("Thông báo", "Cập nhật thành công", "OK");
            }
        }
        public async void LoadData()
        {
            var taiKhoan = await _taiKhoanService.GetByID(id);
            if (taiKhoan != null)
            {
                Username = taiKhoan.TenDangNhap;
                Password = taiKhoan.MatKhau;
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Lỗi, ktìm thấy tài khoản thông qua id hiện tại", "OK");
            }
        }
    }
}
