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
using System.Text.RegularExpressions;
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
        private string tuoi;
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
                Tuoi = result.Tuoi.ToString();
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
            // 0. Validate Tuổi phải parse được và >=1
            if (!int.TryParse(Tuoi, out var tuoiParsed) || tuoiParsed <= 0)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tuổi phải là số nguyên dương", "OK");
                return;
            }

            // 1. Lấy bản gốc
            var original = await _nhanVienService.GetByID(Id);
            if (original != null)
            {
                // So sánh từng field, dùng tuoiParsed thay vì int.Parse
                if (original.CCCD == CCCD
                 && original.GioiTinh == GioiTinh
                 && original.HoTen == HoTen
                 && original.Tuoi == tuoiParsed
                 && original.DiaChi == DiaChi
                 && original.SoDienThoai == SoDienThoai
                 && original.Email == Email)
                {
                    await Shell.Current.DisplayAlert(
                        "Thông báo",
                        "Không có thay đổi nào được thực hiện.",
                        "OK");
                    return;
                }
            }

            // 2. Các validation regex khác
            var cccdPattern = new Regex(@"^\d+$");
            if (!cccdPattern.IsMatch(CCCD))
            {
                await Shell.Current.DisplayAlert("Lỗi", "CCCD chỉ được chứa chữ số", "OK");
                return;
            }
            var namePattern = new Regex(@"^[\p{L}\s]+$");
            if (!namePattern.IsMatch(HoTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Họ tên chỉ được chứa chữ cái", "OK");
                return;
            }
            var phonePattern = new Regex(@"^\d{10,11}$");
            if (!phonePattern.IsMatch(SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại phải từ 10–11 chữ số", "OK");
                return;
            }
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailPattern.IsMatch(Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Email không đúng định dạng", "OK");
                return;
            }

            // 3. Thực hiện cập nhật
            if (original is not null)
            {
                original.CCCD = CCCD;
                original.GioiTinh = GioiTinh;
                original.HoTen = HoTen;
                original.Tuoi = tuoiParsed;   // dùng parsed
                original.DiaChi = DiaChi;
                original.SoDienThoai = SoDienThoai;
                original.Email = Email;
                await _nhanVienService.Update(original);

                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Cập nhật thông tin nhân viên thành công",
                    "OK");
                await ExitUpdate();
                MessagingCenter.Send(this, "ReloadNhanVienList");
            }
            else
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Cập nhật thông tin nhân viên thất bại",
                    "OK");
                await ExitUpdate();
            }
        }


    }
}
