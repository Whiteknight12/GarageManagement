using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietNhanVienPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Guid id = Guid.Empty;
        [ObservableProperty]
        private string cCCD;
        [ObservableProperty]
        private string gioiTinh;
        [ObservableProperty]
        private string hoTen;
        [ObservableProperty]
        private int tuoi;
        [ObservableProperty]
        private string diaChi;
        [ObservableProperty]
        private string soDienThoai;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private Guid? taiKhoanId;

        [ObservableProperty]
        private bool isUpdating = false;
        [ObservableProperty]
        private bool isReadOnly = true;

        [ObservableProperty]
        private ObservableCollection<string> listGioiTinh = new ObservableCollection<string> { "Nam", "Nữ" };

        private readonly APIClientService<NhanVien> _nhanVienService;
        //private readonly QuanLiNhanVienPageViewModel _quanLiNhanVienPageViewModel; 

        public ChiTietNhanVienPageViewModel(APIClientService<NhanVien> nhanVienService)
        {
            _nhanVienService = nhanVienService;
            
        }

        public async Task LoadAsync()
        {
            var result = await _nhanVienService.GetByID(Id);
            if (result is not null)
            {
                CCCD = result.CCCD;
                GioiTinh = result.GioiTinh;
                HoTen = result.HoTen;
                Tuoi = result.Tuoi;
                DiaChi = result.DiaChi;
                SoDienThoai = result.SoDienThoai;
                Email = result.Email;
                TaiKhoanId = result.TaiKhoanId;
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
        public async Task Update()
        {
            var result = await _nhanVienService.GetByID(Id);
            if (result is not null)
            {
                result.CCCD = CCCD;
                result.GioiTinh = GioiTinh;
                result.HoTen = HoTen;
                result.Tuoi = Tuoi; 
                result.DiaChi = DiaChi;
                result.SoDienThoai = SoDienThoai;
                result.Email = Email;
                await _nhanVienService.Update(result);
                var toast = Toast.Make("Cập nhật thông tin khách hàng thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
                await ExitUpdate();
                MessagingCenter.Send(this, "ReloadNhanVienList");
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
