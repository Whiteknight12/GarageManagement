﻿using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class ThuTienPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<KhachHang> listChuXe = new ObservableCollection<KhachHang>();
        [ObservableProperty]
        private Xe selectedBienSo;
        [ObservableProperty]
        private KhachHang selectedChuXe;
        [ObservableProperty]
        private ObservableCollection<Xe> listBienSo = new ObservableCollection<Xe>();
        [ObservableProperty]
        private string dienThoai;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private DateTime ngayThuTien;
        [ObservableProperty]
        private string soTienThu;
        [ObservableProperty]
        private string ten;
        [ObservableProperty]
        public string tenHieuXe;
        [ObservableProperty]
        private string tienNoXeSelected;
        [ObservableProperty]
        private string cCCD;
        [ObservableProperty]
        private string gioiTinh;

        // Thuộc tính mới cho chức năng lọc
        [ObservableProperty]
        private ObservableCollection<string> filterFields = new ObservableCollection<string> { "CCCD", "Tên", "Số điện thoại", "Email" };
        [ObservableProperty]
        private string selectedFilterField;
        [ObservableProperty]
        private string filterValue;
        [ObservableProperty]
        private bool isCustomerFound;
        
        public bool available { get; set; } = false;

        private string role = "Customer";
        private string STORAGE_KEY = "user-account-status";
        private readonly APIClientService<KhachHang> _userservice;
        private readonly APIClientService<PhieuThuTien> _phieuthuservice;
        private readonly APIClientService<Xe> _carservice;

        public ThuTienPageViewModel(APIClientService<KhachHang> userservice,
            APIClientService<PhieuThuTien> phieuthuservice,
            APIClientService<Xe> carservice)
        {
            _userservice = userservice;
            _carservice = carservice;
            _phieuthuservice = phieuthuservice;
            ngayThuTien = DateTime.Now;
            SelectedFilterField = "CCCD"; // Giá trị mặc định
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            var list = await _userservice.GetAll();
            ListChuXe = new ObservableCollection<KhachHang>(list); 
        }

        [RelayCommand]
        private async Task Filter()
        {
            available = true;
            if (string.IsNullOrEmpty(FilterValue))
            {
                await Shell.Current.DisplayAlert("Error", "Vui lòng nhập giá trị để lọc", "OK");
                return;
            }

            IsCustomerFound = false;
            SelectedChuXe = null;
            ListBienSo.Clear();

            var filtered = ListChuXe.FirstOrDefault(kh =>
            {
                return SelectedFilterField switch
                {
                    "CCCD" => kh.CCCD?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Tên" => kh.HoVaTen?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Số điện thoại" => kh.SoDienThoai?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Email" => kh.Email?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    _ => false
                };
            });
            
            if (filtered != null)
            {
                SelectedChuXe = filtered;
                DienThoai = filtered.SoDienThoai;
                Email = filtered.Email;
                CCCD = filtered.CCCD; 
                GioiTinh = filtered.GioiTinh;
                var xeListBySDT = await _carservice.GetListOnSpecialRequirement($"PhoneNumber/{filtered.SoDienThoai}");
                if (xeListBySDT != null)
                {
                    SelectedBienSo = xeListBySDT[0];
                    ListBienSo.Clear();
                    available = false; 
                    ListBienSo = new ObservableCollection<Xe>(xeListBySDT);
                }
                IsCustomerFound = true;
                SelectedBienSo = ListBienSo[0];
                var toast = Toast.Make("Đã lọc thông tin khách hàng và xe liên quan", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy khách hàng", "OK");
            }
            
        }

        [RelayCommand]
        private async Task GoBack()
        {
            //var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            //if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            //var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            //if (currentaccount.Role == "Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }

        [RelayCommand]
        private async Task XacNhan()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống email", "OK");
                return;
            }
            if (SelectedChuXe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống tên khách hàng", "OK");
                return;
            }
            if (SelectedBienSo is null)
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống biển số xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(DienThoai))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống số điện thoại", "OK");
                return;
            }
            if (NgayThuTien.Date < DateTime.Now.Date)
            {
                await Shell.Current.DisplayAlert("Error", "Ngày thu tiền không được nhỏ hơn ngày hiện tại", "OK");
                return;
            }
            if (string.IsNullOrEmpty(SoTienThu))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống số tiền thu", "OK");
                return;
            }
            else
            {
                if (!SoTienThu.All(char.IsDigit))
                {
                    await Shell.Current.DisplayAlert("Error", "Số tiền thu chỉ được chứa số", "OK");
                    return;
                }
                if (double.Parse(SoTienThu) <= 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Số tiền thu phải lớn hơn 0", "OK");
                    return;
                }
            }
            if (double.Parse(SoTienThu) > SelectedBienSo.TienNo)
            {
                await Shell.Current.DisplayAlert("Error", "Không được thu nhiều tiền hơn số tiền nợ của chủ xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(SelectedChuXe.Email) || SelectedChuXe.Email != Email)
            {
                SelectedChuXe.Email = Email;
                await _userservice.Update(SelectedChuXe);
            }
            await _phieuthuservice.Create(new PhieuThuTien
            {
                Id = Guid.NewGuid(),
                NgayThuTien = ngayThuTien,
                SoTienThu = double.Parse(soTienThu),
                XeId = selectedBienSo.Id,
                KhachHangId = selectedChuXe.Id
            });
            SelectedChuXe.TienNo -= double.Parse(SoTienThu);
            SelectedBienSo.TienNo -= double.Parse(SoTienThu);
            if (SelectedBienSo.TienNo == 0) SelectedBienSo.KhaDung = false;
            await _carservice.Update(SelectedBienSo);
            await _userservice.Update(SelectedChuXe);
            TienNoXeSelected = SelectedBienSo.TienNo.ToString();
            SoTienThu = string.Empty; 
            //var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            //if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            //var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            //if (currentaccount.Role == "Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
            var toast = Toast.Make("Thông tin phiếu thu đã được hệ thống lưu trữ.", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
        }
    }
}