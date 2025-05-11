using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ThuTienPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<KhachHang> listChuXe=new ObservableCollection<KhachHang>();
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
            _ = LoadAsync();
            _phieuthuservice = phieuthuservice;
            ngayThuTien=DateTime.Now;
        }
        public async Task LoadAsync()
        {
            var list = await _userservice.GetListOnSpecialRequirement($"GetListThroughRole/{role}");
            if (list is not null)
            {
                listChuXe.Clear();
                foreach (var item in list) listChuXe.Add(item);
            }
        }
        [RelayCommand]
        private async Task GoBack()
        {
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            if (currentaccount.Role=="Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }
        [RelayCommand]
        private async Task XacNhan()
        {
            if (string.IsNullOrEmpty(email))
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong email", "OK");
                return;
            }
            if (selectedChuXe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong ten khach hang", "OK");
                return;
            }
            if (selectedBienSo is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong bien so xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(dienThoai))
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong so dien thoai", "OK");
                return;
            }
            if (ngayThuTien.Date<DateTime.Now.Date)
            {
                await Shell.Current.DisplayAlert("Error", "Ngay thu tien khong duoc nho hon ngay hien tai", "OK");
                return;
            }
            if (string.IsNullOrEmpty(soTienThu))
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong so tien thu", "OK");
                return;
            }
            else
            {
                if (!soTienThu.All(char.IsDigit))
                {
                    await Shell.Current.DisplayAlert("Error", "So tien thu chi duoc chua so", "OK");
                    return;
                }
                if (double.Parse(soTienThu)<=0)
                {
                    await Shell.Current.DisplayAlert("Error", "So tien thu phai lon hon 0", "OK");
                    return;
                }
            }
            if (double.Parse(soTienThu)>selectedBienSo.TienNo)
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc thu nhieu tien hon so tien no cua chu xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(selectedChuXe.Email) || selectedChuXe.Email != email)
            {
                selectedChuXe.Email= email;
                await _userservice.Update(SelectedChuXe);
            }
            await _phieuthuservice.Create(new PhieuThuTien
            {
                Email = email,
                NgayThuTien = ngayThuTien,
                SoTienThu = double.Parse(soTienThu),
                XeId=selectedBienSo.Id,
                KhachHangId=selectedChuXe.Id
            });
            selectedChuXe.TienNo -= double.Parse(soTienThu);
            selectedBienSo.TienNo-= double.Parse(soTienThu);
            if (selectedBienSo.TienNo == 0) selectedBienSo.KhaDung = false;
            await _carservice.Update(selectedBienSo);
            await _userservice.Update(selectedChuXe);
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            if (currentaccount.Role == "Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }
    }
}
