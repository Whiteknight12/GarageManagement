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
        private bool hasAccount;

        public Guid khachHangId;

        private APIClientService<KhachHang> _khachHangService;

        public ChiTietKhachHangViewModel(APIClientService<KhachHang> khachHangService)
        {
            _khachHangService = khachHangService;
        }

        public async void LoadAsync()
        {
            var result=await _khachHangService.GetByID(khachHangId);
            if (result is not null)
            {
                HoVaTen = result.HoVaTen;
                Tuoi = result.Tuoi.ToString() ?? "Chưa nhập";
                DiaChi = result.DiaChi;
                SoDienThoai = result.SoDienThoai;
                TienNo = result.TienNo;
                Email = result.Email ?? "Chưa nhập";
                HasAccount = result.DaCoTaiKhoan;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            var navigationStack = Shell.Current.Navigation.NavigationStack;
            if (navigationStack.Count > 1)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không thể quay lại trang trước", "OK");
            }
        }
    }
}
