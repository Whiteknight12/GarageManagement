using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Globalization;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuThuTienPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<PhieuThuTien> listPhieuThuTien = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<PhieuThuTien> _phieuThuTienService;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<Xe> _xeService;
        private readonly AuthenticationService _authenticationService;
        private readonly ChinhSuaPhieuThuTienPageViewModel _chinhSuaPhieuThuTienPageViewModel;


        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string cccdFilter = string.Empty;
        [ObservableProperty] private string bienSoFilter = string.Empty;

        [ObservableProperty] private DateTime? selectedDate = null;     // null = không lọc ngày

        [ObservableProperty] private string priceFromText = "";
        [ObservableProperty] private string priceToText = "";

        public List<string> DateFilterOptions { get; } = new() { "Tất cả", "Lọc theo ngày" };
        [ObservableProperty] private string selectedDateOption = "Tất cả";
        public bool IsDateVisible => SelectedDateOption == "Lọc theo ngày";
        partial void OnSelectedDateOptionChanged(string _) => ApplyFilter();


        // Khi bất kỳ filter thay đổi => ApplyFilter()
        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnCccdFilterChanged(string _) => ApplyFilter();
        partial void OnBienSoFilterChanged(string _) => ApplyFilter();
        partial void OnSelectedDateChanged(DateTime? _) => ApplyFilter();
        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();

        private List<PhieuThuTien> _allPhieu = new();

        private readonly ThuTienPageViewModel _thuTienPageViewModel;
        private readonly APIClientService<HieuXe> _hieuXeService;

        public QuanLiPhieuThuTienPageViewModel(APIClientService<PhieuThuTien> phieuThuTienService,
            APIClientService<KhachHang> khachHangService,
            APIClientService<Xe> xeService,
            ILogger<QuanLiPhieuThuTienPageViewModel> logger,
            AuthenticationService authenticationService,
            ThuTienPageViewModel thuTienPageViewModel,
            APIClientService<HieuXe> hieuXeService,
            ChinhSuaPhieuThuTienPageViewModel chinhSuaPhieuThuTienPageViewModel)
        {
            _authenticationService = authenticationService;
            _phieuThuTienService = phieuThuTienService;
            _khachHangService = khachHangService;
            _xeService = xeService;
            _thuTienPageViewModel = thuTienPageViewModel;
            _hieuXeService = hieuXeService;
            //_ = LoadAsync();
            IsDeleteMode = false;
            _chinhSuaPhieuThuTienPageViewModel = chinhSuaPhieuThuTienPageViewModel;
            MessagingCenter.Subscribe<ThuTienPageViewModel>(this, "PhieuThuTienCreated", async _ =>
            {
                // refresh your list
                await LoadAsync();
            });
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _phieuThuTienService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var listPhieu = await _phieuThuTienService.GetAll();

            foreach (var pt in listPhieu)
            {
                pt.STT = listPhieu.IndexOf(pt) + 1;
                // Load thông tin khách hàng dựa trên KhachHangId
                var khachHang = await _khachHangService.GetByID(pt.KhachHangId);
                if (khachHang != null)
                {
                    pt.TenKhachHang = khachHang.HoVaTen;
                    pt.CCCD = khachHang.CCCD;
                    pt.Email = khachHang.Email ?? "";
                }
                // Load thông tin xe dựa trên XeId
                var xe = await _xeService.GetByID(pt.XeId);
                if (xe != null)
                {
                    pt.TenXe = xe.Ten;
                    pt.BienSoXe = xe.BienSo;
                }
            }

            _allPhieu = listPhieu;
            ApplyFilter();
        }

        public void Load(PhieuThuTien item)
        {
            ListPhieuThuTien.Add(item);
            // Cập nhật STT cho tất cả phiếu thu tiền
            for (int i = 0; i < ListPhieuThuTien.Count; i++)
            {
                ListPhieuThuTien[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            var view = new ThuTienPage(_thuTienPageViewModel, _xeService, _hieuXeService);
            var page = new ContentPage
            {
                Content = view,
                Padding = 0
            };
            var win = new Window
            {
                Page = page,
                MinimumHeight = 1000,
                MinimumWidth = 1200
            };
            _thuTienPageViewModel.OnPhieuThuTienAdded = async (PhieuThuTien phieuThuTien) =>
            {
                Load(phieuThuTien);        // nhét vào list hiện tại
                                           // hoặc reload full để chắc cú
                                           // Replace this line:
                                           // win.Close();     // đóng cửa sổ
                                           // With the following workaround for .NET MAUI (since Window.Close() does not exist):
                Application.Current?.CloseWindow(win); // đóng cửa sổ    // đóng cửa sổ
                await LoadAsync();
            };
            Application.Current.OpenWindow(win);
        }

        [RelayCommand]
        private void Edit(Guid id)
        {
            // Logic để sửa phiếu thu tiền dựa trên id (có thể mở một trang để chỉnh sửa thông tin)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode) // Khi hủy chế độ xóa
            {
                var updatedList = new List<PhieuThuTien>(ListPhieuThuTien);
                foreach (var pt in updatedList)
                {
                    pt.IsSelected = false;
                }
                // Gán lại ListPhieuThuTien để buộc giao diện làm mới
                ListPhieuThuTien = new ObservableCollection<PhieuThuTien>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListPhieuThuTien.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _phieuThuTienService.Delete(item.Id);
                ListPhieuThuTien.Remove(item);
            }
            // Cập nhật STT sau khi xóa
            for (int i = 0; i < ListPhieuThuTien.Count; i++)
            {
                ListPhieuThuTien[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }

        private void ApplyFilter()
        {
            IEnumerable<PhieuThuTien> q = _allPhieu;

            // Tên KH
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var k = NameFilter.Trim().ToLower();
                q = q.Where(p => (p.TenKhachHang ?? "").ToLower().Contains(k));
            }

            // CCCD
            if (!string.IsNullOrWhiteSpace(CccdFilter))
            {
                var k = CccdFilter.Trim().ToLower();
                q = q.Where(p => (p.CCCD ?? "").ToLower().Contains(k));
            }

            // Biển số
            if (!string.IsNullOrWhiteSpace(BienSoFilter))
            {
                var k = BienSoFilter.Trim().ToLower();
                q = q.Where(p => (p.BienSoXe ?? "").ToLower().Contains(k));
            }

            // Ngày thu tiền
            if (SelectedDate is DateTime d)
            {
                q = q.Where(p => p.NgayThuTien.Date == d.Date);
            }

            // Giá từ / đến
            if (double.TryParse(PriceFromText, NumberStyles.Any, CultureInfo.InvariantCulture, out var min))
                q = q.Where(p => p.SoTienThu >= min);
            if (double.TryParse(PriceToText, NumberStyles.Any, CultureInfo.InvariantCulture, out var max))
                q = q.Where(p => p.SoTienThu <= max);

            ListPhieuThuTien = new ObservableCollection<PhieuThuTien>(q);
        }

        [ObservableProperty] private PhieuThuTien _selectedPhieuThuTien;
        [ObservableProperty] private bool _isDetailPaneVisible;

        partial void OnSelectedPhieuThuTienChanged(PhieuThuTien value)
        {
            IsDetailPaneVisible = value != null;
            // reset chế độ edit
        }

        [RelayCommand]
        public async void ShowDetail(Guid id)
        {
            var item = await _phieuThuTienService.GetByID(id);
            if (item is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu thu tiền", "OK");
                return;
            }
            var khach = await _khachHangService.GetByID(item.KhachHangId);
            var xe = await _xeService.GetByID(item.XeId);
            item.TenKhachHang = khach?.HoVaTen ?? "";
            item.CCCD = khach?.CCCD ?? "";
            item.Email = khach?.Email ?? "";
            item.TenXe = xe?.Ten ?? "";
            item.BienSoXe = xe?.BienSo ?? "";
            SelectedPhieuThuTien = item;
            IsDetailPaneVisible = true; 
        }

        [RelayCommand]
        public void CloseDetail()
        {
            SelectedPhieuThuTien = null;
        }

        [RelayCommand]
        public void EditDetail(Guid id)
        {
            ChinhSuaPhieuThuTienPage view = new ChinhSuaPhieuThuTienPage(_chinhSuaPhieuThuTienPageViewModel, id);

            var win = new Window
            {
                Page = view,
                Title = "Tạo phiếu thu tiền mới",
                MinimumHeight = 600,
                MinimumWidth = 800
            };
            MessagingCenter.Subscribe<ChinhSuaPhieuThuTienPageViewModel>(this, "PhieuThuTienUpdated", async _ =>
            {
                // refresh your list
                await LoadAsync();
                ShowDetail(SelectedPhieuThuTien.Id); 
                // close the **exact** edit window
                Application.Current.CloseWindow(win);

                // unsubscribe so you don’t leak
                MessagingCenter.Unsubscribe<ChinhSuaPhieuThuTienPageViewModel>(this, "PhieuThuTienUpdated");
            });
            //// callback khi lưu thành công
            //_chinhSuaPhieuThuTienPageViewModel.OnPhieuNhapAdded = async (PhieuNhapVatTu phieuNhapVatTu) =>
            //{
            //    Load(phieuNhapVatTu);        // nhét vào list hiện tại
            //                                 // hoặc reload full để chắc cú
            //                                 // Replace this line:
            //                                 // win.Close();     // đóng cửa sổ

            //    // With the following workaround for .NET MAUI (since Window.Close() does not exist):
            //    Application.Current?.CloseWindow(win); // đóng cửa sổ    // đóng cửa sổ
            //    await LoadAsync();
            //};

            Application.Current.OpenWindow(win);
        }
    }
}