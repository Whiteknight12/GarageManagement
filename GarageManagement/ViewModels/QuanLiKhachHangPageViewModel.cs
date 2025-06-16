using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiKhachHangPageViewModel : BaseViewModel
    {
        // ===== LIST & FILTER =====
        [ObservableProperty] private ObservableCollection<KhachHang> listKhachHang = new();
        private List<KhachHang> _allKhachHang = new();

        [ObservableProperty] private string cccdFilter = string.Empty;
        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string phoneFilter = string.Empty;
        [ObservableProperty] private string emailFilter = string.Empty;

        [ObservableProperty] private List<string> listGioiTinh = new List<string>(); 

        partial void OnCccdFilterChanged(string _) => ApplyFilter();
        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnPhoneFilterChanged(string _) => ApplyFilter();
        partial void OnEmailFilterChanged(string _) => ApplyFilter();

        // ===== PANE & EDIT MODE =====
        [ObservableProperty] private bool isAddPaneVisible;
        [ObservableProperty] private bool isEditing;
        [ObservableProperty] private bool isNotEditing = true;   // mặc định đang “xem”
        [ObservableProperty] private KhachHang? editingCustomer; // bind trong pane

        partial void OnIsEditingChanged(bool value) => IsNotEditing = !value;

        // ===== DELETE MODE =====
        [ObservableProperty] private bool isDeleteMode;

        // ===== SERVICE =====
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly AuthenticationService _authenticationService;

        public QuanLiKhachHangPageViewModel(
            APIClientService<KhachHang> khachHangService,
            ILogger<QuanLiKhachHangPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _khachHangService = khachHangService;
            _authenticationService = authenticationService;

            //_ = LoadAsync();

            IsAddPaneVisible = false;
            IsDeleteMode = false;
            ListGioiTinh.Add("Nam");
            ListGioiTinh.Add("Nữ");
        }

        // ------------------ LOAD DATA ------------------
        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _khachHangService.GetHttpClient;
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    _authenticationService.GetCurrentAccountStatus.Token);

            _allKhachHang = await _khachHangService.GetAll() ?? new();

            for (int i = 0; i < _allKhachHang.Count; i++)
                _allKhachHang[i].STT = i + 1;

            ApplyFilter(); // respect bộ lọc hiện tại
        }

        private void ApplyFilter()
        {
            IEnumerable<KhachHang> query = _allKhachHang;

            if (!string.IsNullOrWhiteSpace(CccdFilter))
            {
                var k = CccdFilter.Trim().ToLower();
                query = query.Where(kh => (kh.CCCD ?? "").ToLower().Contains(k));
            }
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var k = NameFilter.Trim().ToLower();
                query = query.Where(kh => (kh.HoVaTen ?? "").ToLower().Contains(k));
            }
            if (!string.IsNullOrWhiteSpace(PhoneFilter))
            {
                var k = PhoneFilter.Trim().ToLower();
                query = query.Where(kh => (kh.SoDienThoai ?? "").ToLower().Contains(k));
            }
            if (!string.IsNullOrWhiteSpace(EmailFilter))
            {
                var k = EmailFilter.Trim().ToLower();
                query = query.Where(kh => (kh.Email ?? "").ToLower().Contains(k));
            }

            ListKhachHang = new ObservableCollection<KhachHang>(query);
        }

        // ------------------ ADD / EDIT FLOW ------------------
        [RelayCommand]
        private void Add()
        {
            EditingCustomer = new KhachHang
            {
                Id = Guid.Empty // mark as “mới toanh”
            };
            IsAddPaneVisible = true;
            IsEditing = true;
        }

        [RelayCommand]
        private void EditDetail()   // gọi khi bấm nút “Sửa” trong page chi tiết
        {
            if (EditingCustomer == null) return;
            IsEditing = true;
        }

        [RelayCommand]
        private async Task SaveCustomerAsync()  // XAML đang bind SaveCustomerCommand
        {
            if (EditingCustomer == null) return;

            if (EditingCustomer.Id == Guid.Empty)
            {
                // ➕ Thêm mới
                await _khachHangService.Create(EditingCustomer);
            }
            else
            {
                // ✏️ Cập nhật
                await _khachHangService.Update(EditingCustomer);
            }

            // reload danh sách & STT
            await LoadAsync();

            // đổi về chế độ xem
            IsEditing = false;
            EditingCustomer = null; 
        }

        [RelayCommand]
        private void CancelCustomer()   // XAML bind CancelCustomerCommand
        {
            CloseDetailPane();
        }

        [RelayCommand]
        private void CloseDetailPane()
        {
            IsAddPaneVisible = false;
            EditingCustomer = null;

            if (IsEditing)
            {
                IsEditing = false;
                IsNotEditing = true;
            }
        }

        // ------------------ DELETE MODE ------------------
        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;

            if (!IsDeleteMode)
            {
                foreach (var kh in ListKhachHang)
                    kh.IsSelected = false;

                // force UI refresh
                ListKhachHang = new ObservableCollection<KhachHang>(ListKhachHang);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selected = ListKhachHang.Where(x => x.IsSelected).ToList();

            foreach (var kh in selected)
            {
                await _khachHangService.Delete(kh.Id);
                ListKhachHang.Remove(kh);
            }

            // rebuild STT
            for (int i = 0; i < ListKhachHang.Count; i++)
                ListKhachHang[i].STT = i + 1;

            IsDeleteMode = false;
            CloseDetailPane();
        }

        // ------------------ NAV TO FULL DETAIL PAGE ------------------
        [RelayCommand]
        private void GoToChiTietKhachHangPage(Guid id)
        {
            MessagingCenter.Send(this, "ShowCustomerDetails", id);
        }
    }
}
