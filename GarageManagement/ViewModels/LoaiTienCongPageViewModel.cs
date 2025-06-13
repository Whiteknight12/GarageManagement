using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class LoaiTienCongPageViewModel : BaseViewModel
    {
        private List<TienCong> _allTienCong = new();

        [ObservableProperty] private ObservableCollection<TienCong> listTienCong = new();

        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string priceFromText = string.Empty;
        [ObservableProperty] private string priceToText = string.Empty;

        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();

        

        [ObservableProperty]
        private bool isDeleteMode;

        private readonly APIClientService<TienCong> _tienCongService;
        private readonly ThemLoaiTienCongPageViewModel _themLoaiTienCongPageViewModel;

        public LoaiTienCongPageViewModel(APIClientService<TienCong> tienCongService,
                                         ThemLoaiTienCongPageViewModel themLoaiTienCongPageViewModel)
        {
            _tienCongService = tienCongService;
            _themLoaiTienCongPageViewModel = themLoaiTienCongPageViewModel;
            IsDeleteMode = false;
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            _allTienCong = await _tienCongService.GetAll() ?? new();
            ApplyFilter();
        }

        /* ========== LOCAL ADD (khi tạo mới) ========== */
        public void Load(TienCong item)
        {
            if (item == null) return;
            if (_allTienCong.Any(x => x.Id == item.Id)) return;

            _allTienCong.Add(item);
            ApplyFilter();
        }

        /* ========== FILTER ========== */
        private void ApplyFilter()
        {
            IEnumerable<TienCong> q = _allTienCong;

            /* ---- theo tên ---- */
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var key = NameFilter.Trim().ToLower();
                q = q.Where(t => (t.TenLoaiTienCong ?? "")
                                 .ToLower()
                                 .Contains(key));
            }

            /* ---- theo giá ---- */
            if (double.TryParse(PriceFromText, out var minPrice))
                q = q.Where(t => t.DonGiaLoaiTienCong >= minPrice);

            if (double.TryParse(PriceToText, out var maxPrice))
                q = q.Where(t => t.DonGiaLoaiTienCong <= maxPrice);

            ListTienCong = new ObservableCollection<TienCong>(q);
        }

        //[RelayCommand]
        //public void AddNewLoaiTienCong(ContentView page)
        //{
        //    if (page is LoaiTienCongPage loaiTienCongPage)
        //    {
        //        var themLoaiTienCongPage = new ThemLoaiTienCongPage(new ThemLoaiTienCongPageViewModel());
        //        MessagingCenter.Send(this, "ShowRightPane", themLoaiTienCongPage);
        //    }
        //}

        [RelayCommand]
        private void Add()
        {
            var newWindow = new Window
            {
                Page = new ThemLoaiTienCongPage(_themLoaiTienCongPageViewModel),
                MaximumHeight = 400,
                MaximumWidth = 400,
                MinimumHeight = 400,
                MinimumWidth = 400
            };

            _themLoaiTienCongPageViewModel.OnTienCongAdded = (TienCong tienCong) =>
            {
                Load(tienCong);
            };

            Application.Current.OpenWindow(newWindow);
        }

        [RelayCommand]
        private void Edit()
        {
            // TODO: Implement logic to edit selected item (gợi ý: chỉ cho phép 1 item được chọn)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            var selected = ListTienCong.Where(x => x.IsSelected).ToList();
            foreach (var t in selected)
            {
                await _tienCongService.Delete(t.Id);
                _allTienCong.Remove(t);
            }
            IsDeleteMode = false;
            ApplyFilter();
        }

        [RelayCommand]
        private void GoToChiTietTienCongPage(Guid id)
        {
            // TODO: Navigate to ChiTietTienCongPage and pass the selected item id
        }

        [RelayCommand]
        private void GoBack()
        {
            Shell.Current.GoToAsync("..");
        }
    }
}
