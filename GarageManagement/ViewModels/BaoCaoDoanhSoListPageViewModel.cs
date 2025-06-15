using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoDoanhSoListPageViewModel : BaseViewModel
    {
        private readonly APIClientService<BaoCaoDoanhThuThang> _baoCaoService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly APIClientService<Xe> _carService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<ChiTietBaoCaoDoanhThuThang> _chiTietService;
        private readonly BaoCaoDoanSoPageViewModel _baoCaoDoanhSoPageViewModel; 

        [ObservableProperty]
        private bool isDetailPaneVisible;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isNotEditing = true;

        [ObservableProperty]
        private BaoCaoDoanhThuThang? selectedBaoCao;

        [ObservableProperty]
        private ObservableCollection<int> months = new();

        [ObservableProperty]
        private ObservableCollection<int> years = new();

        [ObservableProperty]
        private int selectedMonth;

        [ObservableProperty]
        private int selectedYear;

        partial void OnIsEditingChanged(bool value) => IsNotEditing = !value;

        public BaoCaoDoanhSoListPageViewModel(
            APIClientService<BaoCaoDoanhThuThang> baoCaoService,
            APIClientService<PhieuSuaChua> phieuSuaChuaService,
            APIClientService<Xe> carService,
            APIClientService<HieuXe> hieuXeService,
            APIClientService<ChiTietBaoCaoDoanhThuThang> chiTietService,
            BaoCaoDoanSoPageViewModel baoCaoDoanhSoPageViewModel)
        {
            _baoCaoService = baoCaoService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _carService = carService;
            _hieuXeService = hieuXeService;
            _chiTietService = chiTietService;
            _ = LoadAsync();
            _baoCaoDoanhSoPageViewModel = baoCaoDoanhSoPageViewModel;
        }

        [ObservableProperty]
        private ObservableCollection<BaoCaoDoanhThuThang> listBaoCao = new();

        [ObservableProperty]
        private bool isDeleteMode;

        private List<BaoCaoDoanhThuThang> _allBaoCao = new();

        public async Task LoadAsync()
        {
            var list = await _baoCaoService.GetAll();
            _allBaoCao = list ?? new List<BaoCaoDoanhThuThang>();

            _allBaoCao = _allBaoCao
                .OrderBy(bc => bc.Nam)
                .ThenBy(bc => bc.Thang)
                .ToList();

            int index = 1;
            foreach (var bc in _allBaoCao)
                bc.IdForUI = index++;

            ListBaoCao = new ObservableCollection<BaoCaoDoanhThuThang>(_allBaoCao);

            var phieuSuaChua = await _phieuSuaChuaService.GetAll() ?? new List<PhieuSuaChua>();
            Months = new ObservableCollection<int>(phieuSuaChua.Select(p => p.NgaySuaChua.Month).Distinct().OrderBy(m => m));
            Years = new ObservableCollection<int>(phieuSuaChua.Select(p => p.NgaySuaChua.Year).Distinct().OrderBy(y => y));

            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;

            if (!IsDeleteMode)
            {
                foreach (var bc in ListBaoCao)
                    bc.IsSelected = false;

                ListBaoCao = new ObservableCollection<BaoCaoDoanhThuThang>(ListBaoCao);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selected = ListBaoCao.Where(x => x.IsSelected).ToList();

            foreach (var item in selected)
                await _baoCaoService.Delete(item.Id);

            foreach (var item in selected)
                ListBaoCao.Remove(item);

            IsDeleteMode = false;

            int index = 1;
            foreach (var bc in ListBaoCao)
                bc.IdForUI = index++;
        }

        [RelayCommand]
        private async Task ViewDetail(Guid id)
        {
            var baoCao = _allBaoCao.FirstOrDefault(bc => bc.Id == id);
            if (baoCao == null) return;

            var vm = _baoCaoDoanhSoPageViewModel;
            await vm.LoadAsync(baoCao);

            var view = new BaoCaoDoanhSoPage(vm);
            var wrapper = new ContentPage { Content = view, Padding = 0 };

            var win = new Window
            {
                Page = wrapper,
                Title = "Chi tiết báo cáo doanh số",
                MaximumHeight = 700,
                MaximumWidth = 900
            };

            Application.Current.OpenWindow(win);
        }


        [RelayCommand]
        private void Add()
        {
            SelectedBaoCao = new BaoCaoDoanhThuThang
            {
                Id = Guid.Empty
            };

            IsDetailPaneVisible = true;
            IsEditing = true;
        }

        [RelayCommand]
        private void CloseDetailPane()
        {
            IsDetailPaneVisible = false;
            SelectedBaoCao = null;

            if (IsEditing && !IsNotEditing)
            {
                IsEditing = false;
                IsNotEditing = true;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            var now = DateTime.Now;
            if (SelectedMonth == now.Month && SelectedYear == now.Year)
            {
                await Shell.Current.DisplayAlert("Không thể tạo báo cáo", "Tháng hiện tại chưa kết thúc. Vui lòng chọn tháng trước đó.", "OK");
                return;
            }

            var listPhieu = await _phieuSuaChuaService.GetListOnSpecialRequirement($"GetListByMonthAndYear/{SelectedMonth}/{SelectedYear}")
                            ?? new List<PhieuSuaChua>();

            double tongDoanhThu = listPhieu.Sum(p => p.TongTien);

            var newBaoCao = new BaoCaoDoanhThuThang
            {
                Id = Guid.NewGuid(),
                Thang = SelectedMonth,
                Nam = SelectedYear,
                TongDoanhThu = tongDoanhThu
            };

            await _baoCaoService.Create(newBaoCao);

            var revenueByBrand = new Dictionary<Guid, (int SoLuotSua, double ThanhTien)>();

            foreach (var psc in listPhieu)
            {
                var xe = await _carService.GetByID(psc.XeId);
                var hieuXe = xe is null ? null : await _hieuXeService.GetByID(xe.HieuXeId);
                if (hieuXe == null) continue;

                var hieuXeId = hieuXe.Id;

                if (!revenueByBrand.ContainsKey(hieuXeId))
                    revenueByBrand[hieuXeId] = (0, 0);

                revenueByBrand[hieuXeId] = (
                    revenueByBrand[hieuXeId].SoLuotSua + 1,
                    revenueByBrand[hieuXeId].ThanhTien + psc.TongTien
                );
            }

            foreach (var kv in revenueByBrand)
            {
                var ct = new ChiTietBaoCaoDoanhThuThang
                {
                    Id = Guid.NewGuid(),
                    BaoCaoDoanhThuThangId = newBaoCao.Id,
                    HieuXeId = kv.Key,
                    SoLuotSua = kv.Value.SoLuotSua,
                    ThanhTien = kv.Value.ThanhTien,
                    TiLe = (float)(Math.Round(kv.Value.ThanhTien / (tongDoanhThu == 0 ? 1 : tongDoanhThu) * 100, 2))
                };
                await _chiTietService.Create(ct);
            }

            await LoadAsync();
            CloseDetailPane();
        }
    }
}
