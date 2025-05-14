using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
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
        private ObservableCollection<string> listBienSoXe=new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedBienSoXe;

        [ObservableProperty]
        private DateTime ngaySuaChua=DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuSuaChua> listNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();

        [ObservableProperty]
        public ObservableCollection<TienCong> listTienCong=new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTuPhuTung=new ObservableCollection<VatTuPhuTung>();

        [ObservableProperty]
        private double tongThanhTien=0;

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
        }

        public async Task LoadAsync()
        {
            var listcar = await _carservice.GetAll();
            if (listcar is not null)
            {
                ListBienSoXe.Clear();
                foreach (var car in listcar)
                {
                    ListBienSoXe.Add(car.BienSo);
                }
                OnPropertyChanged(nameof(ListBienSoXe));
            }
            var listcong = await _tiencongservice.GetAll();
            if (listcong is not null)
            {
                ListTienCong.Clear();
                foreach (var tiencong in listcong) ListTienCong.Add(tiencong);
                OnPropertyChanged(nameof(ListTienCong));
            }
            var listphutung = await _vattuservice.GetAll();
            if (listphutung is not null)
            {
                ListVatTuPhuTung.Clear();
                foreach (var vattu in listphutung) ListVatTuPhuTung.Add(vattu);
                OnPropertyChanged(nameof(ListVatTuPhuTung));
            }
            if (ListNoiDung.Count==0)
            {
                ListNoiDung.Add(new ChiTietPhieuSuaChua
                {
                    NoiDungId=index++
                });
            }
        }

        [RelayCommand]
        public void ThemNoiDungSuaChua()
        {
            ListNoiDung.Add(new ChiTietPhieuSuaChua
            {
                NoiDungId = index++
            });
        }
        [RelayCommand]
        public async void LuuPhieuSuaChua()
        {
            if (SelectedBienSoXe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong bien so xe", "OK");
                return;
            }
            if (ListNoiDung.Count == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Không có nội dung sữa chữa", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
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
                if (NgaySuaChua.Date<DateTime.Now.Date)
                {
                    await Shell.Current.DisplayAlert("Error", "Ngay sua chua khong duoc nho hon ngay hien tai!", "OK");
                    return;
                }
            }
            Xe xe=await _carservice.GetThroughtSpecialRoute($"BienSo/{SelectedBienSoXe}");
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
            PhieuSuaChua obj=await _phieuservice.Create(new PhieuSuaChua
            {
                Id=Guid.NewGuid(),
                XeId= xe.Id, 
                NgaySuaChua = NgaySuaChua,
                TongTien=TongThanhTien
            });
            if (obj is null)
            {
                await Shell.Current.DisplayAlert("Error", "Tao phieu sua chua moi that bai", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                var chiTietPhieuSuaChua=await _noidungphieuservice.Create(new ChiTietPhieuSuaChua
                {
                    NoiDung = item.NoiDung,
                    PhieuSuaChuaId = obj.Id,
                    VatTuPhuTungId = item.VatTuPhuTungId,
                    TienCongId = item.TienCongId,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.ThanhTien
                });
                var vatTuPhuTung = await _vattuservice.GetByID(item.VatTuPhuTungId??Guid.Empty);
                if (vatTuPhuTung != null) vatTuPhuTung.SoLuong -= item.SoLuong ?? 0;
                await _vattuservice.Update(vatTuPhuTung);
            }
            checkuser.TienNo += TongThanhTien;
            xe.TienNo += TongThanhTien;
            await _userservice.Update(checkuser);
            await _carservice.Update(xe);
            var toast = Toast.Make("Thêm phiếu sữa chữa mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            SelectedBienSoXe = null;
            ListNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();
            TongThanhTien = 0;
        }

        [RelayCommand]
        public void Remove(int itemid)
        {
            var item=ListNoiDung.Where(u=>u.NoiDungId == itemid).FirstOrDefault();
            if (item != null) ListNoiDung.Remove(item);
            TongThanhTien=ListNoiDung.Sum(u=>u.ThanhTien ?? 0);
            OnPropertyChanged(nameof(TongThanhTien));
        }
    }
}
