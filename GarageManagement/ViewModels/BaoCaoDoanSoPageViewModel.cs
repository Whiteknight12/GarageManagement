using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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
        private List<Xe> listXe = new(); 
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
            foreach (var item in listphieusuachua) years.Add(item.NgaySuaChua.Year);
            SelectedYear = DateTime.UtcNow.Year;
            tongDoanhSo = listphieusuachua.Sum(u => u.TongTien);
            _ = GenerateBaoCao();
        }

        public async Task LoadAsync()
        {
            listphieusuachua = await _phieusuachuaservice.GetAll();
            SortedSet<int> tmp= new SortedSet<int>();
            foreach (var item in listphieusuachua)
            {
                tmp.Add(item.NgaySuaChua.Year);
            }
            Years = new ObservableCollection<int>(tmp);
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
                    if (!tmplist.ContainsKey(hieuXe.TenHieuXe))
                    {
                        tmplist[hieuXe.TenHieuXe] = item.TongTien; 
                    }
                    else
                    {
                        tmplist[hieuXe.TenHieuXe] += item.TongTien;
                    }
                }
            }
            Dictionary<Guid, int> id_lan = new();
            foreach (var item in listphieusuachua)
            {
                if (!id_lan.ContainsKey(item.XeId))
                {
                    id_lan[item.XeId] = 1; 
                }
                else
                {
                    id_lan[item.XeId] ++; 
                }
            }
            var listHieuXe = await _hieuXeService.GetAll();
            foreach (var item in tmplist)
            {
                int sum = 0;
                var listcar = await _carservice.GetAll(); 
                
                var hieuXe = listHieuXe.FirstOrDefault(h => h.TenHieuXe == item.Key);
                foreach (var i in id_lan)
                {
                    var car = listcar.FirstOrDefault(c => c.Id == i.Key);
                    if (car != null && car.HieuXeId == hieuXe.Id)
                    {
                        sum += i.Value;
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

      
        [RelayCommand]
        private async Task GoBack()
        {
            //var json = await SecureStorage.GetAsync(STORAGE_KEY);
            //if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", true);
            //var currentaccount=JsonSerializer.Deserialize<taiKhoanSession>(json);
            //if (currentaccount.Role=="Member") await Shell.Current.GoToAsync($"//{nameof(NhanSuMainPage)}", true);
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
