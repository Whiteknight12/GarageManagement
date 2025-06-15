using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoDoanSoPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Chart pieChart;

        [ObservableProperty]
        private List<int> months = new();

        [ObservableProperty]
        private ObservableCollection<int> years = new();

        [ObservableProperty]
        private int selectedMonth;

        [ObservableProperty]
        private int selectedYear;

        [ObservableProperty]
        private double tongDoanhSo;

        [ObservableProperty]
        private ObservableCollection<BaoCaoDoanhThuVM> baoCaoList = new();

        [ObservableProperty]
        private BaoCaoDoanhThuThang? baoCao;

        private readonly APIClientService<PhieuThuTien> _phieuthutienservice;
        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<PhieuSuaChua> _phieusuachuaservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieusuachuaservice;
        private readonly APIClientService<HieuXe> _hieuXeService;

        private List<PhieuSuaChua> listphieusuachua = new();

        public BaoCaoDoanSoPageViewModel(
            APIClientService<PhieuThuTien> phieuthutienservice,
            APIClientService<Xe> carservice,
            APIClientService<PhieuSuaChua> phieusuachuaservice,
            APIClientService<ChiTietPhieuSuaChua> noidungphieusuachuaservice,
            APIClientService<HieuXe> hieuXeService)
        {
            _phieuthutienservice = phieuthutienservice;
            _carservice = carservice;
            _phieusuachuaservice = phieusuachuaservice;
            _noidungphieusuachuaservice = noidungphieusuachuaservice;
            _hieuXeService = hieuXeService;
        }

        public async Task LoadAsync(BaoCaoDoanhThuThang baoCao)
        {
            BaoCao = baoCao;
            SelectedMonth = baoCao.Thang;
            SelectedYear = baoCao.Nam;

            var list = await _phieusuachuaservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{SelectedMonth}/{SelectedYear}");
            listphieusuachua = list ?? new List<PhieuSuaChua>();

            TongDoanhSo = listphieusuachua.Sum(p => p.TongTien);

            await GenerateBaoCao();
        }

        private async Task GenerateBaoCao()
        {
            var revenueByBrand = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            var repairsCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var psc in listphieusuachua)
            {
                var xe = await _carservice.GetByID(psc.XeId);
                var hieuXe = xe is null ? null : await _hieuXeService.GetByID(xe.HieuXeId);
                if (hieuXe == null) continue;

                string brand = hieuXe.TenHieuXe?.Trim() ?? "Không rõ";

                revenueByBrand.TryAdd(brand, 0);
                revenueByBrand[brand] += psc.TongTien;

                repairsCount.TryAdd(brand, 0);
                repairsCount[brand] += 1;
            }

            BaoCaoList.Clear();
            foreach (var kv in revenueByBrand.OrderByDescending(k => k.Value))
            {
                BaoCaoList.Add(new BaoCaoDoanhThuVM
                {
                    HieuXe = kv.Key,
                    ThanhTien = kv.Value,
                    SoLuotSua = repairsCount[kv.Key],
                    TiLe = Math.Round(kv.Value / (TongDoanhSo == 0 ? 1 : TongDoanhSo) * 100, 2)
                });
            }

            UpdateChart();
        }

        private void UpdateChart()
        {
            var entries = BaoCaoList.Select(item => new ChartEntry((float)item.ThanhTien)
            {
                Label = item.HieuXe,
                ValueLabel = item.ThanhTien.ToString("N0"),
                Color = SKColor.Parse(GetRandomColorHex())
            }).ToList();

            PieChart = new PieChart
            {
                Entries = entries,
            };
        }

        private static string GetRandomColorHex()
        {
            var rand = new Random();
            return string.Format("{0:X6}", rand.Next(0x1000000));
        }
    }

    public class BaoCaoDoanhThuVM
    {
        public string HieuXe { get; set; }
        public int SoLuotSua { get; set; }
        public double ThanhTien { get; set; }
        public double TiLe { get; set; }
    }
}
