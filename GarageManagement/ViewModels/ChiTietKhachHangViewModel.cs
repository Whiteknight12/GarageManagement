using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietKhachHangViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string hoVaTen;
        [ObservableProperty]
        private string tuoi;
        [ObservableProperty]
        private string diaChi;
        [ObservableProperty]
        private string soDienThoai;
        [ObservableProperty]
        private double tienNo;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string hasAccount;
        [ObservableProperty]
        private bool isHavingAccount;
        [ObservableProperty]
        private bool isUpdating = false;
        [ObservableProperty]
        private bool isReadOnly = true;
        [ObservableProperty]
        private string cCCD;
        [ObservableProperty]
        private string selectedGioiTinh;
        [ObservableProperty]
        private ObservableCollection<string> listGioiTinh= new ObservableCollection<string> { "Nam", "Nữ" };

        public Guid khachHangId;

        private readonly APIClientService<KhachHang> _khachHangService;

        public ChiTietKhachHangViewModel(APIClientService<KhachHang> khachHangService)
        {
            _khachHangService = khachHangService;
        }

        public async Task LoadAsync()
        {
            var result=await _khachHangService.GetByID(khachHangId);
            if (result is not null)
            {
                CCCD = result.CCCD;
                SelectedGioiTinh=result.GioiTinh;
                HoVaTen = result.HoVaTen;
                Tuoi = result.Tuoi.ToString() ?? "Chưa nhập";
                DiaChi = result.DiaChi;
                SoDienThoai = result.SoDienThoai;
                TienNo = result.TienNo;
                Email = result.Email ?? "Chưa nhập";
                HasAccount = result.DaCoTaiKhoan? "Đã có tài khoản" : "Chưa có tài khoản";
                IsHavingAccount = result.DaCoTaiKhoan;
            }
        }

        [RelayCommand]
        public void ToggleUpdate()
        {
            IsReadOnly = false;
            IsUpdating = true;
        }

        [RelayCommand]
        public async Task ExitUpdate()
        {
            IsReadOnly = true;
            IsUpdating = false;
            await LoadAsync();
        }

        [RelayCommand]
        public async Task UpdateCustomer()
        {
            var result=await _khachHangService.GetByID(khachHangId);
            if (result is not null)
            {
                result.CCCD = CCCD;
                result.GioiTinh = SelectedGioiTinh;
                result.HoVaTen = HoVaTen;
                result.Tuoi = int.TryParse(Tuoi, out var tuoi) ? tuoi : 0;
                result.DiaChi = DiaChi;
                result.SoDienThoai = SoDienThoai;
                result.Email = Email;
                await _khachHangService.Update(result);
                var toast = Toast.Make("Cập nhật thông tin khách hàng thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                MessagingCenter.Send(this, "ReloadCustomerList", result.Id);
                await toast.Show();
                await ExitUpdate();
            }
            else
            {
                var toast = Toast.Make("Cập nhật thông tin khách hàng thất bại", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
                await ExitUpdate();
            }
        }
    }
}
