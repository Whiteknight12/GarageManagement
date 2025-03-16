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

        private readonly APIClientService<Car> _carservice;
        private readonly APIClientService<TienCong> _tiencongservice;
        private readonly APIClientService<VatTuPhuTung> _vattuservice;
        private readonly APIClientService<NoiDungPhieuSuaChua> _noidungphieuservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<User> _userservice;

        [ObservableProperty]
        private ObservableCollection<string> listbiensoxe=new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedbiensoxe;

        [ObservableProperty]
        private DateTime ngaysuachua=DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<NoiDungPhieuSuaChua> listnoidung = new ObservableCollection<NoiDungPhieuSuaChua>();

        [ObservableProperty]
        public ObservableCollection<TienCong> listtiencong=new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listvattuphutung=new ObservableCollection<VatTuPhuTung>();

        [ObservableProperty]
        private double tongthanhtien=0;

        public TaoPhieuSuaChuaPageViewModel(APIClientService<Car> carservice, 
            APIClientService<TienCong> tiencongservice,
            APIClientService<VatTuPhuTung> vattuservice,
            APIClientService<NoiDungPhieuSuaChua> noidungphieuservice,
            APIClientService<PhieuSuaChua> phieuservice,
            APIClientService<User> userservice)
        {
            _carservice = carservice;
            _tiencongservice = tiencongservice;
            _vattuservice = vattuservice;
            _noidungphieuservice = noidungphieuservice;
            _phieuservice = phieuservice;
            _userservice = userservice;
            _ = LoadAsync();
        }
        private async Task LoadAsync()
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
            listnoidung.Add(new NoiDungPhieuSuaChua
            {
                NoiDungID = index++
            });
        }

        [RelayCommand]
        public void ThemNoiDungSuaChua()
        {
            listnoidung.Add(new NoiDungPhieuSuaChua
            {
                NoiDungID = index++
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
                if (item.VatTuPhuTungID is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong vat tu phu tung!", "OK");
                    return;
                }
                if (item.TienCongID is null)
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
            PhieuSuaChua obj=await _phieuservice.Create(new PhieuSuaChua
            {
                BienSoXe = selectedbiensoxe,
                NgaySuaChua=ngaysuachua
            });
            foreach (var item in listnoidung)
            {
                await _noidungphieuservice.Create(new NoiDungPhieuSuaChua
                {
                    NoiDung = item.NoiDung,
                    PhieuSuaChuaID=obj.PhieuSuaChuaID,
                    VatTuPhuTungID=item.VatTuPhuTungID,
                    TienCongID=item.TienCongID,
                    SoLuong=item.SoLuong,
                    DonGia=item.DonGia,
                    ThanhTien=item.ThanhTien
                });
            }
            Car checkcar = await _carservice.GetThroughtSpecialRoute($"GetByBienSo/{selectedbiensoxe}");
            if (checkcar is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim thay xe co bien so tren", "OK");
                return;
            }
            var checkuser = await _userservice.GetThroughtSpecialRoute($"GetByPhoneNumber/{checkcar.DienThoai}");
            if (checkuser is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim chu xe cua xe co bien so tren", "OK");
                return;
            }
            if (checkuser.TienNo is null) checkuser.TienNo = 0;
            checkuser.TienNo += tongthanhtien;
            checkcar.TienNoCuaChuXe += tongthanhtien;
            await _userservice.Update(checkuser);
            await _carservice.Update(checkcar);
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
        public void Remove(int itemid)
        {
            var item=listnoidung.Where(u=>u.NoiDungID == itemid).FirstOrDefault();
            if (item != null) listnoidung.Remove(item);
            tongthanhtien=listnoidung.Sum(u=>u.ThanhTien ?? 0);
            OnPropertyChanged(nameof(tongthanhtien));
        }
    }
}
