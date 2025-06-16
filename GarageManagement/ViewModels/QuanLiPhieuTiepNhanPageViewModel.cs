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

        [ObservableProperty] private PhieuTiepNhan selectedPhieuTiepNhan = null; // null = không chọn phiếu nào

        [ObservableProperty]
        private bool isDetailPaneVisible = false;


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
            SelectedDate = null; 
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
        private async Task ShowRightPane(Guid id)
        {
            var p = await _phieuTiepNhanService.GetByID(id);
            if (p is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu tiếp nhận", "OK");
                return;
            }
            var xe = await _xeService.GetByID(p.XeId); 
            SelectedPhieuTiepNhan = p;
            SelectedPhieuTiepNhan.BienSoXe = xe?.BienSo ?? "";
            OnPropertyChanged(nameof(SelectedPhieuTiepNhan.BienSoXe));
        }

        [RelayCommand]
        private void CloseRightPane()
        {
            SelectedPhieuTiepNhan = null;
            IsDetailPaneVisible = false;
            IsNotEditing = true;
            IsEditing = false;
        }

        [RelayCommand]
        public void Edit(Guid Id)
        {

        }

        private async Task ApplyFilter()
        {
            IEnumerable<PhieuTiepNhan> q = _allPhieu;
            if (q is null || !q.Any())
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có phiếu tiếp nhận nào", "OK");
                return;
            }
            // Ngày
            if (SelectedDate is DateTime d) q = q.Where(p => p.NgayTiepNhan.Value.Date == d.Date);
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
        private PhieuTiepNhan _original;
        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private bool isNotEditing = true; 

        partial void OnSelectedPhieuTiepNhanChanged(PhieuTiepNhan value)
        {
            IsDetailPaneVisible = value != null;
            // reset edit state
            IsEditing = false;
            _ = LoadXeListAsync();
        }

        [RelayCommand]
        private void EditDetailed()
        {
            if (SelectedPhieuTiepNhan == null) return;
            // sao lưu bản gốc
            _original = new PhieuTiepNhan
            {
                Id = SelectedPhieuTiepNhan.Id,
                NgayTiepNhan = SelectedPhieuTiepNhan.NgayTiepNhan,
                XeId = SelectedPhieuTiepNhan.XeId,
                DaHoanThanhBaoTri = SelectedPhieuTiepNhan.DaHoanThanhBaoTri
            };
            IsEditing = true;
            IsNotEditing = false; 
        }

        [RelayCommand]
        private async Task SaveDetailed()
        {
            if (SelectedPhieuTiepNhan == null) return;
            // gọi API Update
            await _phieuTiepNhanService.Update(SelectedPhieuTiepNhan);
            IsEditing = false;
            IsNotEditing = true;
            await LoadAsync(); 
        }

        [RelayCommand]
        private void CancelDetailed()
        {
            if (_original != null)
            {
                // restore bản gốc
                SelectedPhieuTiepNhan.Id = _original.Id;
                SelectedPhieuTiepNhan.NgayTiepNhan = _original.NgayTiepNhan;
                SelectedPhieuTiepNhan.XeId = _original.XeId;
                SelectedPhieuTiepNhan.DaHoanThanhBaoTri = _original.DaHoanThanhBaoTri;
            }
            IsEditing = false;
            IsNotEditing = true; 
        }

        public class XeWithOwner
        {
            public Guid Id { get; init; }
            public string BienSo { get; init; }
            public KhachHang Owner { get; init; }
        }

        [ObservableProperty]
        private ObservableCollection<XeWithOwner> allXe;
        [ObservableProperty]
        private ObservableCollection<XeWithOwner> filteredXe;
        // filter fields
        [ObservableProperty] private string xeBienSoFilter = string.Empty;
        [ObservableProperty] private string xeCccdFilter = string.Empty;
        [ObservableProperty] private string xePhoneFilter = string.Empty;
        [ObservableProperty] private XeWithOwner selectedXeForPhieu;

        partial void OnXeBienSoFilterChanged(string _) => FilterXe();
        partial void OnXeCccdFilterChanged(string _) => FilterXe();
        partial void OnXePhoneFilterChanged(string _) => FilterXe();

        // khi pane hiện hoặc SelectedPhieu thay đổi, mình load lại list Xe
        

        private async Task LoadXeListAsync()
        {
            var xeList = await _xeService.GetAll();
            var temp = new List<XeWithOwner>();
            foreach (var xe in xeList)
            {
                var owner = await _khachHangService.GetByID(xe.KhachHangId);
                temp.Add(new XeWithOwner
                {
                    Id = xe.Id,
                    BienSo = xe.BienSo,
                    Owner = owner
                });
            }
            AllXe = new ObservableCollection<XeWithOwner>(temp);
            FilteredXe = new ObservableCollection<XeWithOwner>();
        }

        partial void OnSelectedXeForPhieuChanged(XeWithOwner xe)
        {
            if (xe is not null && SelectedPhieuTiepNhan != null)
            {
                // Cập nhật giá trị
                SelectedPhieuTiepNhan.XeId = xe.Id;
                SelectedPhieuTiepNhan.BienSoXe = xe.BienSo;

                // Phát notification cho UI:
                OnPropertyChanged(nameof(SelectedPhieuTiepNhan));
                OnPropertyChanged(nameof(SelectedPhieuTiepNhan.XeId));
                OnPropertyChanged(nameof(SelectedPhieuTiepNhan.BienSoXe));
            }
        }



        private void FilterXe()
        {
            if (string.IsNullOrEmpty(XeBienSoFilter) && string.IsNullOrEmpty(XeCccdFilter) && string.IsNullOrEmpty(XePhoneFilter))
            { FilteredXe = new(); return; }
            if (AllXe == null) return;
            var q = AllXe.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(XeBienSoFilter))
                q = q.Where(x => x.BienSo.Contains(XeBienSoFilter, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(XeCccdFilter))
                q = q.Where(x => (x.Owner?.CCCD ?? "")
                                .Contains(XeCccdFilter, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(XePhoneFilter))
                q = q.Where(x => (x.Owner?.SoDienThoai ?? "")
                                .Contains(XePhoneFilter, StringComparison.OrdinalIgnoreCase));
            FilteredXe = new ObservableCollection<XeWithOwner>(q);
        }
    }
}
