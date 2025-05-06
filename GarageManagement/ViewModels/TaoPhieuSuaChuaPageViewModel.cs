using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class TaoPhieuSuaChuaPageViewModel: BaseViewModel
    {
        private int index = 0;
        private string STORAGE_KEY = "user-account-status";

        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<TienCong> _tiencongservice;
        private readonly APIClientService<VatTuPhuTung> _vattuservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieuservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<KhachHang> _userservice;

        [ObservableProperty]
        private ObservableCollection<string> listbiensoxe=new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedbiensoxe;

        [ObservableProperty]
        private DateTime ngaysuachua=DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuSuaChua> listnoidung = new ObservableCollection<ChiTietPhieuSuaChua>();

        [ObservableProperty]
        public ObservableCollection<TienCong> listtiencong=new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listvattuphutung=new ObservableCollection<VatTuPhuTung>();

        [ObservableProperty]
        private double tongthanhtien=0;

        public TaoPhieuSuaChuaPageViewModel(APIClientService<Xe> carservice, 
            APIClientService<TienCong> tiencongservice,
            APIClientService<VatTuPhuTung> vattuservice,
            APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
            APIClientService<PhieuSuaChua> phieuservice,
            APIClientService<KhachHang> userservice)
        {
            _carservice = carservice;
            _tiencongservice = tiencongservice;
            _vattuservice = vattuservice;
            _noidungphieuservice = noidungphieuservice;
            _phieuservice = phieuservice;
            _userservice = userservice;
            _ = LoadAsync();
        }
        public async Task LoadAsync()
        {
            var listcar = await _carservice.GetAll();
            if (listcar is not null)
            {
                listbiensoxe.Clear();
                foreach (var car in listcar)
                {
                    listbiensoxe.Add(car.BienSo);
                }
                OnPropertyChanged(nameof(Listbiensoxe));
            }
            var listcong = await _tiencongservice.GetAll();
            if (listcong is not null)
            {
                listtiencong.Clear();
                foreach (var tiencong in listcong) listtiencong.Add(tiencong);
                OnPropertyChanged(nameof(Listtiencong));
            }
            var listphutung = await _vattuservice.GetAll();
            if (listphutung is not null)
            {
                listvattuphutung.Clear();
                foreach (var vattu in listphutung) listvattuphutung.Add(vattu);
                OnPropertyChanged(nameof(Listvattuphutung));
            }
            listnoidung.Add(new ChiTietPhieuSuaChua
            {
                //tang index cua noi dung duoc them vao 
            });
        }

        [RelayCommand]
        public void ThemNoiDungSuaChua()
        {
            listnoidung.Add(new ChiTietPhieuSuaChua
            {
                //tang index cua noi dung duoc them vao
            });
        }
        [RelayCommand]
        public async void LuuPhieuSuaChua()
        {
            if (selectedbiensoxe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong bien so xe", "OK");
                return;
            } 
            foreach (var item in listnoidung)
            {
                if (string.IsNullOrEmpty(item.NoiDung))
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong noi dung!", "OK");
                    return;
                }
                if (item.VatTuPhuTungId is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong vat tu phu tung!", "OK");
                    return;
                }
                if (item.TienCongId is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong tien cong!", "OK");
                    return;
                }
                if (item.DonGia is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Co ve nhu co loi xay ra", "OK");
                    return;
                }
                string tmp = item.SoLuong.ToString() ?? "";
                if (item.SoLuong is null || !tmp.All(Char.IsDigit))
                {
                    await Shell.Current.DisplayAlert("Error", "So luong khong hop le!", "OK");
                    return;
                }
                if (item.ThanhTien is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Co ve nhu co loi xay ra", "OK");
                    return;
                }
                if (ngaysuachua.Date<DateTime.Now.Date)
                {
                    await Shell.Current.DisplayAlert("Error", "Ngay sua chua khong duoc nho hon ngay hien tai!", "OK");
                    return;
                }
            }
            Xe xe=await _carservice.GetThroughtSpecialRoute($"GetByBienSo/{selectedbiensoxe}");
            PhieuSuaChua obj=await _phieuservice.Create(new PhieuSuaChua
            {
                XeId= xe.Id, 
                NgaySuaChua =ngaysuachua
            });
            foreach (var item in listnoidung)
            {
                await _noidungphieuservice.Create(new ChiTietPhieuSuaChua
                {
                    NoiDung = item.NoiDung,
                    PhieuSuaChuaId=obj.Id,
                    VatTuPhuTungId=item.VatTuPhuTungId,
                    TienCongId=item.TienCongId,
                    SoLuong=item.SoLuong,
                    DonGia=item.DonGia,
                    ThanhTien=item.ThanhTien
                });
            }
            if (xe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim thay xe co bien so tren", "OK");
                return;
            }
            var checkuser = await _userservice.GetByID(xe.KhachHangId);
            if (checkuser is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim chu xe cua xe co bien so tren", "OK");
                return;
            }
            checkuser.TienNo += tongthanhtien;
            xe.TienNo += tongthanhtien;
            await _userservice.Update(checkuser);
            await _carservice.Update(xe);
            await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}");
        }
        [RelayCommand]
        public async void BackToMainPage()
        {
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            var currentaccount = JsonSerializer.Deserialize<UserAccountSession>(json);
            if (currentaccount.Role=="Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}");
        }
        [RelayCommand]
        public void Remove(Guid itemid)
        {
            var item=listnoidung.Where(u=>u.PhieuSuaChuaId == itemid).FirstOrDefault();
            if (item != null) listnoidung.Remove(item);
            tongthanhtien=listnoidung.Sum(u=>u.ThanhTien ?? 0);
            OnPropertyChanged(nameof(tongthanhtien));
        }
    }
}
