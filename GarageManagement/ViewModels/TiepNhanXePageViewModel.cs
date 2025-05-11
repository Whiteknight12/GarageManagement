using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class TiepNhanXePageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenchuxe;
        [ObservableProperty]
        private string tenxe;
        [ObservableProperty]
        private string bienso;
        [ObservableProperty]
        private string diachi;
        [ObservableProperty]
        private string dienthoai;
        [ObservableProperty]
        private DateTime ngaytiepnhan=DateTime.Now;
        [ObservableProperty]
        private ObservableCollection<HieuXe> listmodel = new ObservableCollection<HieuXe>();
        [ObservableProperty]
        private HieuXe selectedmodel;

        private string STORAGE_KEY = "user-account-status";
        private readonly APIClientService<PhieuTiepNhan> _recordservice;
        private readonly APIClientService<ThamSo> _ruleservice;
        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<HieuXe> _hieuxeservice;
        private readonly APIClientService<KhachHang> _userservice;
        private readonly UniqueConstraintCheckingService _checkservice;
        public TiepNhanXePageViewModel(APIClientService<PhieuTiepNhan> recordservice, 
            APIClientService<ThamSo> ruleservice, 
            APIClientService<Xe> carservice,
            APIClientService<HieuXe> hieuxeservice,
            APIClientService<KhachHang> userservice)
        {
            _recordservice = recordservice;
            _ruleservice = ruleservice;
            _carservice = carservice;
            _hieuxeservice = hieuxeservice;
            _userservice = userservice;
            LoadAsync();
        }

        public async Task LoadAsync()
        {
            var list = await _hieuxeservice.GetAll();
            if (list is not null)
            {
                Listmodel.Clear(); 
                foreach (var item in list)
                {
                    Listmodel.Add(item); 
                }
            }
            OnPropertyChanged(nameof(listmodel));
        }

        [RelayCommand]
        public async void AddNewCarRecord()
        {
            if (string.IsNullOrWhiteSpace(tenchuxe))
            {
                await Shell.Current.DisplayAlert("Error", "Ten Chu Xe Khong Duoc Bo Trong", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(tenxe))
            {
                await Shell.Current.DisplayAlert("Error", "Ten Xe Khong Duoc Bo Trong", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(bienso))
            {
                await Shell.Current.DisplayAlert("Error", "Bien So Khong Duoc Bo Trong", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(selectedmodel.TenHieuXe))
            {
                await Shell.Current.DisplayAlert("Error", "Hieu Xe Khong Duoc Bo Trong", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(diachi))
            {
                await Shell.Current.DisplayAlert("Error", "Dia Chi Khong Duoc Bo Trong", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(dienthoai) || !dienthoai.All(char.IsDigit))
            {
                await Shell.Current.DisplayAlert("Error", "So Dien Thoai Khong Hop Le", "OK");
                return;
            }
            if (ngaytiepnhan == null)
            {
                await Shell.Current.DisplayAlert("Error", "Ngay Tiep Nhan Khong Duoc Bo Trong", "OK");
                return;
            }
            if (ngaytiepnhan.Date<DateTime.Today)
            {
                await Shell.Current.DisplayAlert("Error", "Ngay tiep nhan khong duoc nho hon ngay hien tai", "OK");
                return;
            }
            ThamSo rulemodel = await _ruleservice.GetThroughtSpecialRoute("GetRule");
            List<PhieuTiepNhan> carRecords = await _recordservice.GetAll();
            int c = 0;
            foreach (PhieuTiepNhan record in carRecords)
            {
                if (record.NgayTiepNhan.Value.Date == DateTime.Today) c++;
            }
            if (c>=rulemodel.SoXeTiepNhanToiDaMotNgay)
            {
                await Shell.Current.DisplayAlert("Error", "Vuot qua so xe tiep nhan toi da mot ngay", "OK");
                return;
            }
            var checkcar = await _carservice.GetThroughtSpecialRoute($"GetByBienSo/{bienso}");
            if (checkcar is null)
            {
                KhachHang khachHang=await _userservice.GetThroughtSpecialRoute($"GetByPhoneNumber/{dienthoai}");
                await _carservice.Create(new Xe
                {
                    Ten = tenxe,
                    HieuXeId=selectedmodel.Id,
                    BienSo = bienso,
                    KhachHangId= khachHang.Id,
                    KhaDung = true,
                    TienNo=0 
                });
            }
            else
            {
                checkcar.KhaDung = true;
                await _carservice.Update(checkcar);
            }
            var xe=await _carservice.GetThroughtSpecialRoute($"GetByBienSo/{bienso}");
            await _recordservice.Create(new PhieuTiepNhan
            {
                XeId = xe.Id,
                NgayTiepNhan = ngaytiepnhan
            });
            var checkuser = await _userservice.GetThroughtSpecialRoute($"GetByPhoneNumber/{dienthoai}");
            if (checkuser is null) await _userservice.Create(new KhachHang
            {
                HoVaTen=tenchuxe,
                DiaChi=diachi,
                SoDienThoai=dienthoai,
                DaCoTaiKhoan=false,
                TienNo=0
            });
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            if (currentaccount.Role == "User") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }

        [RelayCommand]
        private async void Back()
        {
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            var currentaccount=JsonSerializer.Deserialize<taiKhoanSession>(json);
            if (currentaccount.Role=="User") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }
    }
}
