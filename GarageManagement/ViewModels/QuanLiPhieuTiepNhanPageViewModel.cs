using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuTiepNhanPageViewModel : BaseViewModel
    {
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<KhachHang> _khachHangService;

        private readonly APIClientService<ThamSo> _ruleService;

        private readonly APIClientService<HieuXe> _hieuxeService;

        private readonly APIClientService<NhomNguoiDung> _groupService;


        [ObservableProperty]
        private ObservableCollection<PhieuTiepNhan> listPhieuTiepNhan = new ObservableCollection<PhieuTiepNhan>();

        private List<PhieuTiepNhan> _allPhieu = new();

        public List<string> DateFilterOptions { get; } = new() { "Tất cả", "Lọc theo ngày" };
        [ObservableProperty] private string selectedDateOption = "Tất cả";
        public bool IsDateVisible => SelectedDateOption == "Lọc theo ngày";
        partial void OnSelectedDateOptionChanged(string _) => ApplyFilter();


        // ============================ FILTERS =========================
        [ObservableProperty] private DateTime? selectedDate = null;        // null = không lọc ngày
        [ObservableProperty] private string bienSoFilter = string.Empty;
        [ObservableProperty] private string cccdFilter = string.Empty;
        [ObservableProperty] private string nameFilter = string.Empty;

        // Khi thay đổi → lọc lại
        partial void OnSelectedDateChanged(DateTime? _) => ApplyFilter();
        partial void OnBienSoFilterChanged(string _) => ApplyFilter();
        partial void OnCccdFilterChanged(string _) => ApplyFilter();
        partial void OnNameFilterChanged(string _) => ApplyFilter();

        private readonly TiepNhanXePageViewModel _tiepNhanXePageViewModel; 

        public QuanLiPhieuTiepNhanPageViewModel(APIClientService<PhieuTiepNhan> phieuTiepNhanService,
            APIClientService<Xe> xeService,
            APIClientService<KhachHang> khachHangService,
            TiepNhanXePageViewModel tiepNhanXePageViewModel,
            APIClientService<ThamSo> ruleService,
            APIClientService<HieuXe> hieuxeService,
             APIClientService<NhomNguoiDung> groupService)
        {
            _phieuTiepNhanService = phieuTiepNhanService;
            _xeService = xeService;
            _khachHangService = khachHangService;
            _tiepNhanXePageViewModel = tiepNhanXePageViewModel;
            _ruleService = ruleService;
            _hieuxeService = hieuxeService;
            _groupService = groupService;

            MessagingCenter.Subscribe<TiepNhanXePageViewModel>(this, "ReloadData", async _ =>
            {
                // refresh your list
                await LoadAsync();

                // unsubscribe so you don’t leak
                MessagingCenter.Unsubscribe<TiepNhanXePageViewModel>(this, "ReloadData");
            });
        }

        public async Task LoadAsync()
        {
            var list = await _phieuTiepNhanService.GetAll();
            if (list is not null)
            {
                foreach (var item in list)
                {
                    if (item.DaHoanThanhBaoTri) item.TinhTrang = "Lần tiếp nhận này đã hoàn thành bảo trì";
                    else item.TinhTrang = "Xe được tiếp nhận còn trong Gara để bảo trì";
                    var xe = await _xeService.GetByID(item.XeId);
                    if (xe is not null)
                    {
                        item.BienSoXe = xe.BienSo;
                        var kh = await _khachHangService.GetByID(xe.KhachHangId);
                        if (kh is not null)
                        {
                            item.TenKhachHang = kh.HoVaTen;
                            item.KhachHangId = kh.Id;
                        }
                    }
                    _allPhieu = list;
                    ApplyFilter();
                    IndexCalculator(ListPhieuTiepNhan);
                }
            }
        }

        [ObservableProperty] private bool isDeleteMode;
        [RelayCommand] private void ToggleDeleteMode() => IsDeleteMode = !IsDeleteMode;

        [RelayCommand]
        private async Task Delete()
        {
            var selected = ListPhieuTiepNhan.Where(p => p.IsSelected).ToList();
            foreach (var item in selected)
                await _phieuTiepNhanService.Delete(item.Id);   // hoặc API xoá của bạn

            foreach (var item in selected)
                ListPhieuTiepNhan.Remove(item);

            IsDeleteMode = false;
        }


        private void IndexCalculator(ObservableCollection<PhieuTiepNhan> list)
        {
            int c = 1;
            foreach (var item in list)
            {
                item.IdForUI = c++;
                item.OnPropertyChanged(nameof(item.IdForUI));
            }
        }

        [RelayCommand]
        public void ShowXeDetail(Guid XeId)
        {
            MessagingCenter.Send(this, "ShowCarDetails", XeId);
        }

        [RelayCommand]
        public void ShowKhachHangDetail(Guid KhachHangId)
        {
            MessagingCenter.Send(this, "ShowCustomerDetails", KhachHangId);
        }

        [RelayCommand]
        public async void Edit(Guid Id)
        {

        }

        private async Task ApplyFilter()
        {
            IEnumerable<PhieuTiepNhan> q = _allPhieu;

            // Ngày
            if (SelectedDate is DateTime d)
                q = q.Where(p => p.NgayTiepNhan.Value.Date == d.Date);

            // Biển số
            if (!string.IsNullOrWhiteSpace(BienSoFilter))
            {
                var k = BienSoFilter.Trim().ToLower();
                q = q.Where(p => (p.BienSoXe ?? "").ToLower().Contains(k));
            }

            // CCCD
            if (!string.IsNullOrWhiteSpace(CccdFilter))
            {
                var k = CccdFilter.Trim().ToLower();
                var temp = new List<PhieuTiepNhan>(); 
                foreach (var item in q)
                {
                    if (item.KhachHangId is not null)
                    {
                        var kh = await _khachHangService.GetByID(item.KhachHangId.Value);
                        if (kh != null && (kh.CCCD ?? "").ToLower().Contains(k))
                        {
                            temp.Add(item);
                        }
                    }
                }
                q = temp.AsEnumerable();
            }

            // Tên KH
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var k = NameFilter.Trim().ToLower();
                q = q.Where(p => (p.TenKhachHang ?? "").ToLower().Contains(k));
            }

            ListPhieuTiepNhan = new ObservableCollection<PhieuTiepNhan>(q);
        }
        [RelayCommand]
        private async Task Add()
        {
            var view = new TiepNhanXePage(_phieuTiepNhanService,
       _ruleService,
        _xeService,
        _hieuxeService,
        _khachHangService,
        _groupService, _tiepNhanXePageViewModel);

            var wrapper = new ContentPage { Content = view, Padding = 0 };
            var editWindow = new Window
            {
                Page = wrapper,
                Title = "Thêm phiếu tiếp nhận mới",
                MinimumHeight = 600,
                MinimumWidth = 800
            };
            Application.Current.OpenWindow(editWindow);

            // 2) Subscribe to the update message, closing *this* editWindow
            

        }
    }
}
