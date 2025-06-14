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
    public partial class QuanLiPhieuNhapPageViewModel : BaseViewModel
    {
        public ObservableCollection<string> TimeFilterOptions { get; } =
        new ObservableCollection<string> { "Tất cả", "Ngày", "Tháng", "Năm" };

        [ObservableProperty] private string selectedTimeFilter = "Tất cả";

        [ObservableProperty] private DateTime selectedDate = DateTime.UtcNow;   // cho “Ngày”

        [ObservableProperty] private string priceFromText = "";
        [ObservableProperty] private string priceToText = "";
        private readonly APIClientService<VatTuPhuTung> _vatTuService;
        private readonly LapPhieuNhapPageViewModel _lapPhieuNhapPageViewModel; 

        // helper cho Visible
        public bool IsDayFilter => SelectedTimeFilter == "Tất cả";
        public bool IsMonthFilter => SelectedTimeFilter == "Tháng";

 
        public bool IsDateVisible => SelectedTimeFilter != "Tất cả";


        // giữ danh sách gốc
        private List<PhieuNhapVatTu> _allPhieu = new();

        partial void OnSelectedTimeFilterChanged(string _) => ApplyFilter();
        partial void OnSelectedDateChanged(DateTime _) => ApplyFilter();
        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();

        [ObservableProperty]
        private ObservableCollection<PhieuNhapVatTu> listPhieuNhapVatTu = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<PhieuNhapVatTu> _PhieuNhapVatTuService;
        private readonly AuthenticationService _authenticationService;

        public QuanLiPhieuNhapPageViewModel(APIClientService<PhieuNhapVatTu> PhieuNhapVatTuService,
            ILogger<QuanLiPhieuNhapPageViewModel> logger,
            AuthenticationService authenticationService,
            LapPhieuNhapPageViewModel lapPhieuNhapPageViewModel,
            APIClientService<VatTuPhuTung> vatTuService)
        {
            _authenticationService = authenticationService;
            _PhieuNhapVatTuService = PhieuNhapVatTuService;
            _lapPhieuNhapPageViewModel = lapPhieuNhapPageViewModel;
            _vatTuService = vatTuService; 
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _PhieuNhapVatTuService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _PhieuNhapVatTuService.GetAll();

            foreach (var ps in list)
            {
                ps.STT = list.IndexOf(ps) + 1;
            }

            _allPhieu = list;         // bổ sung
            ApplyFilter();
        }

        public void Load(PhieuNhapVatTu item)
        {
            ListPhieuNhapVatTu.Add(item);
            _allPhieu.Add(item);
            for (int i = 0; i < _allPhieu.Count; i++)
            {
                _allPhieu[i].STT = i + 1;
            }
            for (int i = 0; i < ListPhieuNhapVatTu.Count; i++)
            {
                ListPhieuNhapVatTu[i].STT = i + 1;
            }
        }

        private void ApplyFilter()
        {
            IEnumerable<PhieuNhapVatTu> q = _allPhieu;

            /*––– LỌC THỜI GIAN –––*/
            if (SelectedTimeFilter == "Ngày")
            {
                q = q.Where(p => p.NgayNhap.Date == SelectedDate.Date);
            }
            else if (SelectedTimeFilter == "Tháng")
            {
                q = q.Where(p => p.NgayNhap.Month == SelectedDate.Month &&
                                     p.NgayNhap.Year == SelectedDate.Year);
            }
            else if (SelectedTimeFilter == "Năm")
            {
                q = q.Where(p => p.NgayNhap.Year == SelectedDate.Year);
            }
            // "Tất cả" thì không lọc gì hết

            /*––– LỌC GIÁ –––*/
            if (double.TryParse(PriceFromText, out var min))
                q = q.Where(p => p.TongTien >= min);

            if (double.TryParse(PriceToText, out var max))
                q = q.Where(p => p.TongTien <= max);

            ListPhieuNhapVatTu = new ObservableCollection<PhieuNhapVatTu>(q);
        }

        [RelayCommand]
        private async Task Add()
        {
            await _lapPhieuNhapPageViewModel.LoadListVatTuAsync();

            var view = new LapPhieuNhapPage(_lapPhieuNhapPageViewModel, _vatTuService);

            var wrapper = new ContentPage
            {
                Content = view,
                Padding = 0
            };

            var win = new Window
            {
                Page = wrapper,
                Title = "Tạo phiếu nhập mới",
                MaximumHeight = 600,
                MaximumWidth = 800,
                MinimumHeight = 600,
                MinimumWidth = 800
            };

            // callback khi lưu thành công
            _lapPhieuNhapPageViewModel.OnPhieuNhapAdded = async (PhieuNhapVatTu phieuNhapVatTu) =>
            {
                Load(phieuNhapVatTu);        // nhét vào list hiện tại
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

        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode)
            {
                var updatedList = new List<PhieuNhapVatTu>(ListPhieuNhapVatTu);
                foreach (var ps in updatedList)
                {
                    ps.IsSelected = false;
                }

                ListPhieuNhapVatTu = new ObservableCollection<PhieuNhapVatTu>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListPhieuNhapVatTu.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _PhieuNhapVatTuService.Delete(item.Id);
                ListPhieuNhapVatTu.Remove(item);
            }

            for (int i = 0; i < ListPhieuNhapVatTu.Count; i++)
            {
                ListPhieuNhapVatTu[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }

        [RelayCommand]
        public void ViewDetailPhieuNhap(Guid Id)
        {
            MessagingCenter.Send(this, "ViewChiTietPhieuNhap", Id);
        }
    }
}
