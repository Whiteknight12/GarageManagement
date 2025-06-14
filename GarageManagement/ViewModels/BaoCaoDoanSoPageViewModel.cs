using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoDoanSoPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private Chart pieChart;

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
        private List<Xe> listXe = new(); 
        private readonly APIClientService<PhieuSuaChua> _phieusuachuaservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieusuachuaservice;
        private readonly APIClientService<HieuXe> _hieuXeService; 
        private string STORAGE_KEY = "user-account-status";

        public BaoCaoDoanSoPageViewModel(
        APIClientService<PhieuThuTien> phieuthutienservice,
        APIClientService<Xe> carservice,
        APIClientService<PhieuSuaChua> phieusuachuaservce,
        APIClientService<ChiTietPhieuSuaChua> noidungphieusuachuaservice,
        APIClientService<HieuXe> hieuXeService)
        {
            _phieuthutienservice = phieuthutienservice;
            _carservice = carservice;
            _phieusuachuaservice = phieusuachuaservce;
            _noidungphieusuachuaservice = noidungphieusuachuaservice;
            _hieuXeService = hieuXeService;

            // Khởi gán danh sách tháng
            Months = Enumerable.Range(1, 12).ToList();

            // chọn mặc định
            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;

            // CHỈ gọi 1 lần, không gọi GenerateBaoCao ở đây
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            listphieusuachua = await _phieusuachuaservice.GetAll() ?? new List<PhieuSuaChua>();

            Years = new ObservableCollection<int>(
                        listphieusuachua
                        .Select(p => p.NgaySuaChua.Year)
                        .Distinct()
                        .OrderBy(y => y));

            OnPropertyChanged(nameof(Years));

            // Gán sau khi Years đã được tạo → đảm bảo SelectedYear nằm trong danh sách
            if (Years.Contains(DateTime.Now.Year))
                SelectedYear = DateTime.Now.Year;
            else
                SelectedYear = Years.FirstOrDefault();  // fallback

            TongDoanhSo = listphieusuachua.Sum(p => p.TongTien);

            await GenerateBaoCao();
        }


        private async Task GenerateBaoCao()
        {
            // ---------- 1. Gom doanh thu theo hiệu xe (chuẩn hoá Trim + ToUpper) ----------
            var revenueByBrand = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            var repairsCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var psc in listphieusuachua)
            {
                var xe = await _carservice.GetByID(psc.XeId);
                var hieuXe = xe is null ? null : await _hieuXeService.GetByID(xe.HieuXeId);
                if (hieuXe == null) continue;

                string brand = hieuXe.TenHieuXe?.Trim() ?? "Không rõ";

                // cộng doanh thu
                revenueByBrand.TryAdd(brand, 0);
                revenueByBrand[brand] += psc.TongTien;

                // đếm lượt sửa
                repairsCount.TryAdd(brand, 0);
                repairsCount[brand] += 1;
            }

            // ---------- 2. Đổ vào ObservableCollection (xóa – nạp lại) ----------
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
                // You can set BackgroundColor or LabelTextSize if needed
                // BackgroundColor = SKColors.White,
                // LabelTextSize = 32
            };
        }

        private static string GetRandomColorHex()
        {
            var rand = new Random();
            return string.Format("{0:X6}", rand.Next(0x1000000));
        }

        public async Task OnDateChanged()
        {
            var newlistPhieuSuaChua = await _phieusuachuaservice.GetListOnSpecialRequirement($"GetListByMonthAndYear/{selectedMonth}/{selectedYear}");
            if (newlistPhieuSuaChua != null)
            {
                listphieusuachua.Clear(); 
                foreach (var item in newlistPhieuSuaChua)
                {
                    listphieusuachua.Add(item);
                }
                TongDoanhSo = listphieusuachua.Sum(u => u.TongTien);
            }
            await GenerateBaoCao();
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
