using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoDoanSoPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private List<int> months=new List<int>();
        [ObservableProperty]
        private ObservableCollection<int> years=new ObservableCollection<int>();
        [ObservableProperty]
        private int selectedMonth;
        [ObservableProperty]
        private int selectedYear;
        [ObservableProperty]
        private double tongDoanhSo;
        [ObservableProperty]
        private ObservableCollection<BaoCaoDoanhThuVM> baoCaoList = new ObservableCollection<BaoCaoDoanhThuVM>();

        private readonly APIClientService<PhieuThuTien> _phieuthutienservice;
        private readonly APIClientService<Xe> _carservice;
        private List<PhieuThuTien> listphieuthutien = new List<PhieuThuTien>();
        private List<PhieuSuaChua> listphieusuachua = new List<PhieuSuaChua>();
        private readonly APIClientService<PhieuSuaChua> _phieusuachuaservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieusuachuaservice;
        private readonly APIClientService<HieuXe> _hieuXeService; 
        private string STORAGE_KEY = "user-account-status";

        public BaoCaoDoanSoPageViewModel(APIClientService<PhieuThuTien> phieuthutienservice,
            APIClientService<Xe> carservice,
            APIClientService<PhieuSuaChua> phieusuachuaservce,
            APIClientService<ChiTietPhieuSuaChua> noidungphieusuachuaservice,
            APIClientService<HieuXe> hieuXeService)
        {
            for (int i = 1; i <= 12; i++) months.Add(i);
            SelectedMonth = DateTime.Now.Month;
            _phieuthutienservice = phieuthutienservice;
            _carservice = carservice;
            _phieusuachuaservice = phieusuachuaservce;
            _noidungphieusuachuaservice = noidungphieusuachuaservice;
            _hieuXeService = hieuXeService;
            _ = LoadAsync();
            if (listphieusuachua.Any())
            {
                foreach (var item in listphieusuachua) years.Add(item.NgaySuaChua.Year);
            }
            SelectedYear = DateTime.Now.Year;
            tongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu);
            _ = GenerateBaoCao();
        }

        public async Task LoadAsync()
        {
            var list=await _phieusuachuaservice.GetAll();
            SortedSet<int> tmp= new SortedSet<int>();
            foreach (var item in list)
            {
                tmp.Add(item.NgaySuaChua.Year);
            }
            years.Clear();
            foreach (var item in tmp) years.Add(item);
        }

        private async Task GenerateBaoCao()
        {
            BaoCaoList.Clear();
            Dictionary<string, double> tmplist= new Dictionary<string, double>();
            foreach (var item in listphieusuachua)
            {
                var car = await _carservice.GetByID(item.XeId);
                if (car != null)
                {
                    HieuXe hieuXe= await _hieuXeService.GetByID(car.HieuXeId);
                    tmplist[hieuXe.TenHieuXe] = 0;
                    var tmp=await _phieuthutienservice.GetListOnSpecialRequirement($"GetListByBienSo/{car.BienSo}");
                    if (tmp != null) tmplist[hieuXe.TenHieuXe] += tmp.Sum(u => u.SoTienThu);
                }
            }
            foreach (var item in tmplist)
            {
                int sum = 0;
                var listcar = await _carservice.GetListOnSpecialRequirement($"GetListByHieuXe/{item.Key}");
                if (listcar != null)
                {
                    foreach(var caritem in listcar)
                    {
                        var listphieusuachua = await _phieusuachuaservice.GetListOnSpecialRequirement($"GetListByBienSoXe/{caritem.BienSo}");
                        foreach (var phieu in listphieusuachua)
                        {
                            if (phieu.NgaySuaChua.Month == selectedMonth && phieu.NgaySuaChua.Year == selectedYear) sum++;
                        }
                    }
                }
                BaoCaoList.Add(new BaoCaoDoanhThuVM
                {
                    HieuXe = item.Key,
                    ThanhTien = item.Value,
                    SoLuotSua = sum,
                    TiLe = item.Value / TongDoanhSo * 100
                });
            }
        }
        
        public async Task OnMonthChanged()
        {
            var list = await _phieuthutienservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{selectedMonth}/{selectedYear}");
            var newlist=await _phieusuachuaservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{selectedMonth}/{selectedYear}");
            if (list is not null)
            {
                listphieuthutien.Clear(); 
                foreach (var item in list)
                {
                    listphieuthutien.Add(item);
                }
                TongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu);
            }
            if (newlist is not null)
            {
                listphieusuachua.Clear();
                foreach (var item in newlist)
                {
                    listphieusuachua.Add(item);
                }
            }
            await GenerateBaoCao();
        }

        public async Task OnYearChanged()
        {
            var list = await _phieuthutienservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{selectedMonth}/{selectedYear}");
            if (list is not null)
            {
                listphieuthutien.Clear();
                foreach (var item in list)
                {
                    listphieuthutien.Add(item);
                }
                TongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu);
            }
            var newlist=await _phieusuachuaservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{selectedMonth}/{selectedYear}");
            if (newlist is not null)
            {
                listphieusuachua.Clear();
                foreach (var item in newlist)
                {
                    listphieusuachua.Add(item);
                }
            }
            await GenerateBaoCao();
        }
        [RelayCommand]
        private async Task GoBack()
        {
            var json = await SecureStorage.GetAsync(STORAGE_KEY);
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            var currentaccount=JsonSerializer.Deserialize<UserAccountSession>(json);
            if (currentaccount.Role=="Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
        }
    }
    public class BaoCaoDoanhThuVM
    {
        public string HieuXe {  get; set; }
        public int SoLuotSua { get; set; }
        public double ThanhTien { get ; set; }
        public double TiLe { get; set; }
    }
}
