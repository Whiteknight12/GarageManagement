using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiXePageViewModel : BaseViewModel
    {
        [ObservableProperty] private string bienSoFilter = string.Empty;
        [ObservableProperty] private string cccdFilter = string.Empty;

        // dropdown hiệu xe
        [ObservableProperty] private List<string> listHieuXe = new();
        [ObservableProperty] private string? selectedHieuXe;     // null = tất cả

        // tiền nợ
        [ObservableProperty] private string debtFromText = string.Empty;
        [ObservableProperty] private string debtToText = string.Empty;

        // auto-trigger
        partial void OnBienSoFilterChanged(string _) => ApplyFilter();
        partial void OnCccdFilterChanged(string _) => ApplyFilter();
        partial void OnSelectedHieuXeChanged(string? _) => ApplyFilter();
        partial void OnDebtFromTextChanged(string _) => ApplyFilter();
        partial void OnDebtToTextChanged(string _) => ApplyFilter();

        private bool _tapLocked = false;
        private string STORAGE_KEY = "user-account-status";
        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieuservice;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly AuthenticationService _authenticationService;
        private readonly ThemXePageViewModel _themXePageViewModel;


        private List<Xe> _allXe = new();
        [ObservableProperty]
        private ObservableCollection<Xe> listcar = new ObservableCollection<Xe>();

        [ObservableProperty] private Xe? selectedXe;
        [ObservableProperty] private bool isDetailPaneVisible;
        [ObservableProperty] private bool isEditing;
        [ObservableProperty] private bool isNotEditing = true;
        [ObservableProperty] private bool isDeleteMode = false;
        [ObservableProperty] private bool isViewing = true;
        partial void OnIsEditingChanged(bool v) => IsNotEditing = !v;

        public QuanLiXePageViewModel(APIClientService<Xe> carservice,
            APIClientService<PhieuSuaChua> phieuservice,
            APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
            AuthenticationService authenticationService,
            APIClientService<KhachHang> khachHangService,
            APIClientService<HieuXe> hieuXeService,
            ThemXePageViewModel themXePageViewModel)
        {
            _authenticationService = authenticationService;
            _carservice = carservice;
            //_ = LoadAsync();
            _phieuservice = phieuservice;
            _noidungphieuservice = noidungphieuservice;
            _khachHangService = khachHangService;
            _hieuXeService = hieuXeService;
            _themXePageViewModel = themXePageViewModel;
            MessagingCenter.Subscribe<ChiTietXePageViewModel>(
                    this, "XeUpdated",
                    async _ => await LoadAsync());
        }

        private void ApplyFilter()
        {
            IEnumerable<Xe> q = _allXe;

            if (!string.IsNullOrWhiteSpace(BienSoFilter))
            {
                var k = BienSoFilter.Trim().ToLower();
                q = q.Where(x => (x.BienSo ?? "").ToLower().Contains(k));
            }

            if (!string.IsNullOrWhiteSpace(CccdFilter))
            {
                var k = CccdFilter.Trim().ToLower();
                q = q.Where(x => (x.CCCDChuXe ?? "").ToLower().Contains(k));
            }

            // chỉ lọc khi chọn khác "Tất cả"
            if (!string.IsNullOrWhiteSpace(SelectedHieuXe) &&
                !SelectedHieuXe.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                q = q.Where(x => (x.TenHieuXe ?? "")
                        .Equals(SelectedHieuXe, StringComparison.OrdinalIgnoreCase));
            }


            if (double.TryParse(DebtFromText, out var min))
                q = q.Where(x => x.TienNo >= min);

            if (double.TryParse(DebtToText, out var max))
                q = q.Where(x => x.TienNo <= max);

            Listcar = new ObservableCollection<Xe>(q);
        }

        [RelayCommand]
        private async Task ViewDetailXe(Guid id)
        {
            if (_tapLocked) { _tapLocked = false; return; }

            // 1) tìm xe trong cache
            var xe = _allXe.FirstOrDefault(x => x.Id == id);
            if (xe == null) return;

            // 2) lấy thông tin liên quan (song song cho nhanh)
            var ownerTask = _khachHangService.GetByID(xe.KhachHangId);
            var brandTask = _hieuXeService.GetByID(xe.HieuXeId);

            var owner = await ownerTask;
            var brand = await brandTask;

            // 3) copy vào object
            xe.TenChuXe = owner?.HoVaTen ?? "";
            xe.CCCDChuXe = owner?.CCCD ?? "";
            xe.TenHieuXe = brand?.TenHieuXe ?? "";

            // 4) BÂY GIỜ mới gán cho UI
            SelectedXe = xe;
            IsDetailPaneVisible = true;
            IsEditing = false;
            IsNotEditing = true;


            var chuxe = await _khachHangService.GetByID(SelectedXe.KhachHangId);
            SelectedXe.CCCDChuXe = chuxe?.CCCD ?? "";
            SelectedXe.TenChuXe = chuxe?.HoVaTen ?? "";
            var hx = await _hieuXeService.GetByID(SelectedXe.HieuXeId);
            SelectedXe.TenHieuXe = hx?.TenHieuXe ?? "";
            IsDetailPaneVisible = true;
            IsEditing = false;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedXe == null)
            {
                await CloseDetailPane();
                IsEditing = false;
                IsNotEditing = true;
                IsDetailPaneVisible = false;
                IsViewing = true;
            }
            if (SelectedXe != null)
            {
                await _carservice.Create(SelectedXe);
                await LoadAsync();
                await CloseDetailPane();
                IsEditing = false;
                IsNotEditing = true;
                IsDetailPaneVisible = false;
                IsViewing = true;
            }
        }
        [RelayCommand]
        private async Task SaveXeAsync()
        {
            if (SelectedXe == null) return;

            if (SelectedXe.Id == Guid.Empty)
                await _carservice.Create(SelectedXe);
            else
                await _carservice.Update(SelectedXe);

            await LoadAsync();
            IsEditing = false;
            IsNotEditing = true;
            SelectedXe = null;
            IsDetailPaneVisible = false;
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
        }
        [RelayCommand]
        private async Task CloseDetailPane()
        {
            IsDetailPaneVisible = false;
            SelectedXe = null;
            IsEditing = false;
            IsNotEditing = true;
            IsViewing = true;

            _tapLocked = true;           // chặn tap rơi
            await Task.Delay(150);
            _tapLocked = false;
        }

        [RelayCommand] private async Task CancelXe() => await CloseDetailPane();

        [RelayCommand]
        private void Edit()
        {
            IsEditing = true;
            IsNotEditing = false;
            IsViewing = false;
        }

        public async Task LoadAsync()
        {
            /* 1. Lấy token & danh sách xe 1 lần */
            _ = _authenticationService.FettaiKhoanSession();
            _carservice.GetHttpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    _authenticationService.GetCurrentAccountStatus.Token);

            _allXe = await _carservice.GetAll() ?? new List<Xe>();

            /* 2. Enrich thông tin chủ xe / hiệu xe song song */
            var enrichTasks = _allXe.Select(async xe =>
            {
                var ownerTask = _khachHangService.GetByID(xe.KhachHangId);
                var brandTask = _hieuXeService.GetByID(xe.HieuXeId);
                await Task.WhenAll(ownerTask, brandTask);

                xe.TenChuXe = ownerTask.Result?.HoVaTen ?? "";
                xe.CCCDChuXe = ownerTask.Result?.CCCD ?? "";
                xe.TenHieuXe = brandTask.Result?.TenHieuXe ?? "";
                return xe;
            });

            var mappedList = await Task.WhenAll(enrichTasks);   // tất cả xong cùng lúc

            /* 3. Gán 1 lần cho UI – tránh flicker */
            Listcar = new ObservableCollection<Xe>(mappedList);

            /* 4. Nạp dropdown hiệu xe */
            var hieuXeList = mappedList
                .Select(x => x.TenHieuXe ?? "")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s)
                .ToList();
            hieuXeList.Insert(0, "Tất cả");                     // thêm “Tất cả” ở đầu

            // Lưu tùy chọn cũ (nếu còn)
            var prev = SelectedHieuXe;

            SelectedHieuXe = null;      // reset để PropertyChanged chắc chắn bắn
            ListHieuXe = hieuXeList;
            SelectedHieuXe = hieuXeList.FirstOrDefault(x =>
                                x.Equals(prev, StringComparison.OrdinalIgnoreCase))
                             ?? "Tất cả";    // nếu prev không còn, về “Tất cả”

            /* 5. Áp dụng bộ lọc hiện tại */
            ApplyFilter();
        }



        [RelayCommand]
        private async Task GoToChiTietXePage(int parameter)
        {
            await Shell.Current.GoToAsync($"//{nameof(ChiTietXePage)}?parameterID={parameter}", true);
        }

        [RelayCommand]
        private async Task GoBack()
        {
            var json = await SecureStorage.Default.GetAsync(STORAGE_KEY);
            if (json is null) return;
            if (string.IsNullOrEmpty(json)) await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            var currentaccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
        }

        [RelayCommand]
        private async Task Add()
        {
            await _themXePageViewModel.LoadAsync();
            ThemXePage themXePage = new ThemXePage(_themXePageViewModel);
            var win = new Window
            {
                Page = themXePage,
                Title = "Thêm xe mới",
                MaximumHeight = 1000,
                MaximumWidth = 1200,
                MinimumHeight = 1000,
                MinimumWidth = 1200
            };
            _themXePageViewModel.OnXeAdded = async (Xe xe) =>
            {
                // With the following workaround for .NET MAUI (since Window.Close() does not exist):
                Application.Current?.CloseWindow(win); // đóng cửa sổ    // đóng cửa sổ
                await LoadAsync();
            };
            Application.Current?.OpenWindow(win); // mở cửa sổ mới
        }

        [RelayCommand]
        public void ShowXeDetail(Guid XeId)
        {
            MessagingCenter.Send(this, "ShowCarDetails", XeId);
        }

    }
}
