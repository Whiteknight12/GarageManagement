using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

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

        [ObservableProperty]
        private string tuoiInput = string.Empty;

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
        private async Task SaveCustomerAsync()
        {
            if (EditingCustomer == null) return;

            // 1. CCCD bắt buộc
            if (string.IsNullOrWhiteSpace(EditingCustomer.CCCD))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập CCCD", "OK");
                return;
            }
            // chỉ chứa chữ số
            var cccdPattern = new Regex(@"^\d+$");
            if (!cccdPattern.IsMatch(EditingCustomer.CCCD))
            {
                await Shell.Current.DisplayAlert("Lỗi", "CCCD chỉ được chứa chữ số", "OK");
                return;
            }

            // 2. Họ và tên bắt buộc
            if (string.IsNullOrWhiteSpace(EditingCustomer.HoVaTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập họ và tên", "OK");
                return;
            }
            // chỉ chứa chữ cái và khoảng trắng
            var namePattern = new Regex(@"^[\p{L}\s]+$");
            if (!namePattern.IsMatch(EditingCustomer.HoVaTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Họ và tên chỉ được chứa chữ cái", "OK");
                return;
            }

            // Trong SaveCustomerAsync()
            if (string.IsNullOrWhiteSpace(TuoiInput))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập tuổi", "OK");
                return;
            }
            // chỉ số nguyên dương
            if (!Regex.IsMatch(TuoiInput, @"^\d+$"))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tuổi phải là số nguyên dương", "OK");
                return;
            }
            var tuoiParsed = int.Parse(TuoiInput);
            EditingCustomer.Tuoi = tuoiParsed;

            // 3. Giới tính bắt buộc
            if (string.IsNullOrWhiteSpace(EditingCustomer.GioiTinh))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng chọn giới tính", "OK");
                return;
            }

            // 4. Số điện thoại bắt buộc và định dạng 10–11 chữ số
            if (string.IsNullOrWhiteSpace(EditingCustomer.SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập số điện thoại", "OK");
                return;
            }
            var phonePattern = new Regex(@"^\d{10,11}$");
            if (!phonePattern.IsMatch(EditingCustomer.SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại phải từ 10–11 chữ số", "OK");
                return;
            }

            // 5. Email bắt buộc và hợp lệ
            if (string.IsNullOrWhiteSpace(EditingCustomer.Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập email", "OK");
                return;
            }
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailPattern.IsMatch(EditingCustomer.Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Email không đúng định dạng", "OK");
                return;
            }

            // pass hết validation thì tạo mới hoặc cập nhật
            if (EditingCustomer.Id == Guid.Empty)
            {
                await _khachHangService.Create(EditingCustomer);
            }
            else
            {
                await _khachHangService.Update(EditingCustomer);
            }

            // thông báo success
            var toast = Toast.Make("Lưu khách hàng thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();

            // reload danh sách & STT
            await LoadAsync();

            // đóng pane & reset state
            IsEditing = false;
            IsNotEditing = true;
            CloseDetailPane();
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
