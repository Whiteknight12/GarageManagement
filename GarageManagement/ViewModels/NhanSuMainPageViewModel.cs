using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using GarageManagement.Services;
using System.Text.Json;

namespace GarageManagement.ViewModels
{
    public partial class NhanSuMainPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenNguoiDung;
        [ObservableProperty]
        private string tenTaiKhoan;
        [ObservableProperty]
        private string tuoi;
        [ObservableProperty]
        private string diaChi;
        [ObservableProperty]
        private string soDienThoai;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string hoVaTen;
        [ObservableProperty]
        private string gioiTinh;
        [ObservableProperty]
        private string role;
        [ObservableProperty]
        private string avatarUrl;

        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly AuthenticationService _authenticationServices;

        public NhanSuMainPageViewModel(APIClientService<NhanVien> nhanVienService, AuthenticationService authenticationService)
        {
            _nhanVienService=nhanVienService;
            _authenticationServices=authenticationService;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationServices.FettaiKhoanSession();
            var currentAccount = _authenticationServices.GetCurrentAccountStatus;
            var currentAccountId = currentAccount?.AccountId ?? Guid.Empty;
            
            var result = await _nhanVienService.GetThroughtSpecialRoute("TaiKhoanId", currentAccountId.ToString());
            TenNguoiDung = result?.HoTen ?? "";
            TenTaiKhoan = currentAccount?.Username ?? "";
            Tuoi = result?.Tuoi.ToString() ?? "";
            DiaChi = result?.DiaChi ?? "";
            SoDienThoai = result?.SoDienThoai ?? "";
            Email = result?.Email ?? "";
            HoVaTen = TenNguoiDung;
            if (result?.GioiTinh == "Nam") AvatarUrl = "male_staff_icon.png";
            else AvatarUrl = "female_staff_icon.png";
        }
    }
}
