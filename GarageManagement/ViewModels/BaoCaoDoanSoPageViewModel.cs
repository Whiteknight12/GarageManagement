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
        private readonly APIClientService<Car> _carservice;
        private List<PhieuThuTien> listphieuthutien = new List<PhieuThuTien>();
        private readonly APIClientService<PhieuSuaChua> _phieusuachuaservce;
        private readonly APIClientService<NoiDungPhieuSuaChua> _noidungphieusuachuaservice;

        public BaoCaoDoanSoPageViewModel(APIClientService<PhieuThuTien> phieuthutienservice,
            APIClientService<Car> carservice,
            APIClientService<PhieuSuaChua> phieusuachuaservce,
            APIClientService<NoiDungPhieuSuaChua> noidungphieusuachuaservice)
        {
            for (int i = 1; i <= 12; i++) months.Add(i);
            SelectedMonth = DateTime.Now.Month;
            _phieuthutienservice = phieuthutienservice;
            _carservice = carservice;
            _phieusuachuaservce = phieusuachuaservce;
            _noidungphieusuachuaservice = noidungphieusuachuaservice;
            _ = LoadAsync();
            if (listphieuthutien.Any())
            {
                foreach (var item in listphieuthutien) years.Add(item.NgayThuTien.Value.Year);
            }
            SelectedYear = DateTime.Now.Year;
            tongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu) ?? 0;
            _ = GenerateBaoCao();
        }

        private async Task LoadAsync()
        {
            var list = await _phieuthutienservice.GetAll();
            SortedSet<int> tmp= new SortedSet<int>();
            foreach (var item in list)
            {
                tmp.Add(item.NgayThuTien.Value.Year);
            }
            foreach (var item in tmp) years.Add(item);
        }

        private async Task GenerateBaoCao()
        {
            BaoCaoList.Clear();
            Dictionary<string, double> tmplist= new Dictionary<string, double>();
            foreach (var item in listphieuthutien)
            {
                var car = await _carservice.GetThroughtSpecialRoute($"GetByBienSo/{item.BienSoXe}");
                if (car != null)
                {
                    if (!tmplist.ContainsKey(car.Model))
                    {
                        tmplist[car.Model] = 0; 
                    }
                    tmplist[car.Model] += item.SoTienThu ?? 0;
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
                        var listphieusuachua = await _phieusuachuaservce.GetListOnSpecialRequirement($"GetListByBienSoXe/{caritem.BienSo}");
                        foreach (var phieu in listphieusuachua)
                        {
                            if (phieu.NgaySuaChua.Value.Month == selectedMonth && phieu.NgaySuaChua.Value.Year == selectedYear) sum++;
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
            if (list is not null)
            {
                listphieuthutien.Clear(); 
                foreach (var item in list)
                {
                    listphieuthutien.Add(item);
                }
                TongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu) ?? 0;
                await GenerateBaoCao();
            }
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
                TongDoanhSo = listphieuthutien.Sum(u => u.SoTienThu) ?? 0;
                await GenerateBaoCao();
            }
        }
        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
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
