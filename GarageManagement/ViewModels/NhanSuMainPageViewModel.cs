using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        private ObservableCollection<ThongBao> danhSachThongBao = new ObservableCollection<ThongBao>();

        public Guid NguoiDungId;

        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly AuthenticationService _authenticationServices;
        private readonly APIClientService<ThongBao> _thongBaoService;
        private readonly APIClientService<NguoiDungThongBao> _ndtbService;
        private readonly APIClientService<NhomNguoiDung> _roleService;
        private readonly APIClientService<KhachHang> _khService;
        private readonly APIClientService<NhanVien> _nvService;

        public NhanSuMainPageViewModel(APIClientService<NhanVien> nhanVienService, 
            AuthenticationService authenticationService,
            APIClientService<ThongBao> thongBaoService,
            APIClientService<NguoiDungThongBao> ndtbService,
            APIClientService<NhomNguoiDung> roleService,
            APIClientService<KhachHang> khService,
            APIClientService<NhanVien> nvService)
        {
            _nhanVienService = nhanVienService;
            _authenticationServices = authenticationService;
            _thongBaoService = thongBaoService;
            _ndtbService = ndtbService;
            _roleService = roleService;
            _khService = khService;
            _nvService = nvService;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationServices.FettaiKhoanSession();
            var currentAccount = _authenticationServices.GetCurrentAccountStatus;
            var currentAccountId = currentAccount?.AccountId ?? Guid.Empty;
            
            var result = await _nhanVienService.GetThroughtSpecialRoute("TaiKhoanId", currentAccountId.ToString());
            if (result is not null)
            {
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
            else
            {
                var kh = await _khService.GetThroughtSpecialRoute($"account-id/{currentAccountId}");
                if (kh is not null)
                {
                    TenNguoiDung = kh?.HoVaTen ?? "";
                    TenTaiKhoan = currentAccount?.Username ?? "";
                    Tuoi = kh?.Tuoi.ToString() ?? "";
                    DiaChi = kh?.DiaChi ?? "";
                    SoDienThoai = kh?.SoDienThoai ?? "";
                    Email = kh?.Email ?? "";
                    HoVaTen = TenNguoiDung;
                    if (kh?.GioiTinh == "Nam") AvatarUrl = "male_staff_icon.png";
                    else AvatarUrl = "female_staff_icon.png";
                }
            }

            var listRole = await _roleService.GetAll();
            var role = listRole.FirstOrDefault(u => u.TenNhom == currentAccount?.Role);
            if (role is null)
            {
                return;
            }
            var listThongBao = await _thongBaoService.GetListOnSpecialRequirement($"NhomNguoiDungId/{role.Id}");
            if (listThongBao is not null)
            {
                var accountId = currentAccount?.AccountId;
                var nguoiDung1 = await _khService.GetThroughtSpecialRoute($"account-id/{accountId}");
                var nguoiDung2 = await _nvService.GetThroughtSpecialRoute($"TaiKhoanId/{accountId}");
                Guid nguoiDungId= Guid.Empty;
                if (nguoiDung1 is not null) nguoiDungId = nguoiDung1.Id;
                else if (nguoiDung2 is not null) nguoiDungId = nguoiDung2.Id;
                NguoiDungId= nguoiDungId;
                foreach (var item in listThongBao)
                {
                    var target = await _ndtbService.GetThroughtSpecialRoute($"nguoiDungIdAndThongBaoId/{nguoiDungId}/{item.Id}");
                    if (target is null)
                    {
                        item.DaDoc = false;
                        item.Visible = true;
                    }
                    else
                    {
                        item.DaDoc = true;
                        item.Visible = false;
                    }
                }
                var sortedList = listThongBao
                    .OrderBy(tb => tb.DaDoc ?? true) 
                    .ThenByDescending(tb => tb.taoVaoLuc)
                    .ToList();
                DanhSachThongBao.Clear();
                foreach (var item in sortedList)
                    DanhSachThongBao.Add(item);
            }
        }

        [RelayCommand]
        private void ScrollToRight()
        {
            MessagingCenter.Send(this, "ScrollRight");
        }

        [RelayCommand]
        private void ScrollToLeft()
        {
            MessagingCenter.Send(this, "ScrollLeft");
        }
    }
}
