using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuSuaChuaPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<PhieuSuaChua> listPhieuSuaChua = new();

        [ObservableProperty] private string bienSoFilter = string.Empty;
        [ObservableProperty] private DateTime selectedRepairDate = DateTime.Today;
        [ObservableProperty] private string priceFromText = string.Empty;
        [ObservableProperty] private string priceToText = string.Empty;

        public ObservableCollection<string> TimeFilterOptions { get; } =
    new ObservableCollection<string> { "Tất cả", "Ngày" };

        [ObservableProperty]
        private string selectedTimeFilter = "Tất cả";


        // Khi các field thay đổi → lọc lại
        partial void OnBienSoFilterChanged(string _) => ApplyFilter();
        partial void OnSelectedRepairDateChanged(DateTime _) => ApplyFilter();
        partial void OnSelectedTimeFilterChanged(string _) => ApplyFilter();

        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();

        private List<PhieuSuaChua> _allPhieu = new();

        public bool IsDateVisible => SelectedTimeFilter == "Ngày";

        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaChuaService;
        private readonly AuthenticationService _authenticationService;
        private readonly APIClientService<Xe> _xeService; // Service để lấy thông tin xe
        private readonly TaoPhieuSuaChuaPageViewModel _taoPhieuSuaChuaPageViewModel;

        APIClientService<TienCong> _congservice;
        APIClientService<ChiTietPhieuSuaChua> _noidungphieuservice;
        APIClientService<VatTuPhuTung> _vatTuService;

        public QuanLiPhieuSuaChuaPageViewModel(APIClientService<PhieuSuaChua> phieuSuaChuaService,
            APIClientService<Xe> xeService,
            ILogger<QuanLiPhieuSuaChuaPageViewModel> logger,
            AuthenticationService authenticationService,
            TaoPhieuSuaChuaPageViewModel taoPhieuSuaChuaPageViewModel)
        {
            _authenticationService = authenticationService;
            _phieuSuaChuaService = phieuSuaChuaService;
            _xeService = xeService;
            _taoPhieuSuaChuaPageViewModel = taoPhieuSuaChuaPageViewModel; 
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _phieuSuaChuaService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var listPhieu = await _phieuSuaChuaService.GetAll();

            foreach (var ps in listPhieu)
            {
                ps.STT = listPhieu.IndexOf(ps) + 1;
                // Load thông tin xe dựa trên XeId
                var xe = await _xeService.GetByID(ps.XeId);
                if (xe != null)
                {
                    ps.TenXe = xe.Ten; // Giả định Xe có thuộc tính TenXe
                    ps.BienSoXe = xe.BienSo; // Giả định Xe có thuộc tính BienSoXe
                }
            }

            _allPhieu = listPhieu;   // lưu danh sách gốc
            ApplyFilter();
        }

        public void Load(PhieuSuaChua item)
        {
            ListPhieuSuaChua.Add(item);
            // Cập nhật STT cho tất cả phiếu sửa chữa
            for (int i = 0; i < ListPhieuSuaChua.Count; i++)
            {
                ListPhieuSuaChua[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            var view = new TaoPhieuSuaChuaPage(_xeService, _congservice, _vatTuService, _noidungphieuservice, _phieuSuaChuaService ,_taoPhieuSuaChuaPageViewModel, _vatTuService);
            var wrapper = new ContentPage
            {
                Content = view,
                Padding = 0
            };
            var win = new Window
            {
                Page = wrapper,
                Title = "Tạo sửa chữa mới",
                MaximumHeight = 600,
                MaximumWidth = 800,
                MinimumHeight = 600,
                MinimumWidth = 800
            };
            _taoPhieuSuaChuaPageViewModel.OnPhieuSuaChuaAdded = async (PhieuSuaChua phieuSuaChua) =>
            {
                Load(phieuSuaChua);        // nhét vào list hiện tại
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
            // Logic để sửa phiếu sửa chữa dựa trên id (có thể mở một trang để chỉnh sửa thông tin)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode) // Khi hủy chế độ xóa
            {
                var updatedList = new List<PhieuSuaChua>(ListPhieuSuaChua);
                foreach (var ps in updatedList)
                {
                    ps.IsSelected = false;
                }
                // Gán lại ListPhieuSuaChua để buộc giao diện làm mới
                ListPhieuSuaChua = new ObservableCollection<PhieuSuaChua>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListPhieuSuaChua.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _phieuSuaChuaService.Delete(item.Id);
                ListPhieuSuaChua.Remove(item);
            }
            // Cập nhật STT sau khi xóa
            for (int i = 0; i < ListPhieuSuaChua.Count; i++)
            {
                ListPhieuSuaChua[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }

        [RelayCommand]
        private void ViewDetailPhieuSuaChua(Guid id)
        {
           MessagingCenter.Send(this, "ViewChiTietPhieuSuaChua", id);
        }

        private void ApplyFilter()
        {
            IEnumerable<PhieuSuaChua> q = _allPhieu;

            /* --- Biển số --- */
            if (!string.IsNullOrWhiteSpace(BienSoFilter))
            {
                var key = BienSoFilter.Trim().ToLower();
                q = q.Where(p => (p.BienSoXe ?? "").ToLower().Contains(key));
            }

            /* --- Ngày sửa chữa --- */
            if (SelectedTimeFilter == "Ngày" && SelectedRepairDate != DateTime.MinValue)
            {
                var d = SelectedRepairDate.Date;
                q = q.Where(p => p.NgaySuaChua.Date == d);
            }
            // "Tất cả" thì bỏ qua lọc ngày

            /* --- Giá từ / đến --- */
            if (double.TryParse(PriceFromText, out var min))
                q = q.Where(p => (p.TongTien) >= min);

            if (double.TryParse(PriceToText, out var max))
                q = q.Where(p => (p.TongTien) <= max);

            ListPhieuSuaChua = new ObservableCollection<PhieuSuaChua>(q);
        }

    }
}